using Godot;
using System;
using System.Linq;

public partial class networking : Control
{
	[Export]
	private int port = 8910;

	[Export]
	private string address = "127.0.0.1";

	private ENetMultiplayerPeer peer;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Multiplayer.PeerConnected += PeerConnected;
		Multiplayer.PeerDisconnected += PeerDisconnected;
		Multiplayer.ConnectedToServer += ConnectedToServer;
		Multiplayer.ConnectionFailed += ConnectionFailed;
		if(OS.GetCmdlineArgs().Contains("--server")){
			hostGame();
		}
	}

	
	private void ConnectionFailed()
	{
		GD.Print("CONNECTION FAILED");
	}
	
	private void ConnectedToServer()
	{
		GD.Print("Connected To Server");
		RpcId(1, "sendPlayerInformation", GetNode<LineEdit>("LineEdit").Text, Multiplayer.GetUniqueId());
	}

	private void PeerDisconnected(long id)
	{
		GD.Print("Player Disconnected: " + id.ToString());
		GameManager.Players.Remove(GameManager.Players.Where(i => i.Id == id).First<PlayerInfo>());
		var players = GetTree().GetNodesInGroup("Player");
		
		foreach (var item in players)
		{
			if(item.Name == id.ToString()){
				item.QueueFree();
			}
		}
	}
	
	private void PeerConnected(long id)
	{
		GD.Print("Player Connected! " + id.ToString());
	}
	
	private void hostGame(){
		peer = new ENetMultiplayerPeer();
		var error = peer.CreateServer(port, 2);
		if(error != Error.Ok){
			GD.Print("error cannot host! :" + error.ToString());
			return;
		}
		peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);

		Multiplayer.MultiplayerPeer = peer;
		GD.Print("Waiting For Players!");
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	private void _on_host_button_down()
	{
		// Replace with function body.
		hostGame();
		sendPlayerInformation(GetNode<LineEdit>("LineEdit").Text, 1);
	
	}


	private void _on_join_button_down()
	{
		peer = new ENetMultiplayerPeer();
		peer.CreateClient(address, port);

		peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);
		Multiplayer.MultiplayerPeer = peer;
		GD.Print("Joining Game!");

	}


	private void _on_start_button_down()
	{
		Rpc("startGame");
	}
	
	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
	private void startGame(){

		var scene = ResourceLoader.Load<PackedScene>("res://Asset/Map/Map1.tscn").Instantiate<Node2D>();
		GetTree().Root.AddChild(scene);
		this.Hide();
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer)]
	private void sendPlayerInformation(string name, int id){
		PlayerInfo playerInfo = new PlayerInfo(){
			Name = name,
			Id = id
		};
		
		if(!GameManager.Players.Contains(playerInfo)){
			
			GameManager.Players.Add(playerInfo);
			
		}

		if(Multiplayer.IsServer()){
			foreach (var item in GameManager.Players)
			{
				Rpc("sendPlayerInformation", item.Name, item.Id);
			}
		}
	}

}



