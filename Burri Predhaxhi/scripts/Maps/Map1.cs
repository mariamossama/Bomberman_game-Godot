using Godot;
using System;
using System.Linq;
using System.Collections.Generic; 
public partial class Map1 : Node2D
{
	[Export]
	private PackedScene playerScene;
	//PackedScene bombScene;
	[Export]
	private PackedScene bombScene;
	private List<Player> players = new List<Player>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		int index = 0;
		//bombScene = GD.Load<PackedScene>( "res://Bomb.tscn");
		foreach (var item in GameManager.Players)
		{
			Player currentPlayer = playerScene.Instantiate<Player>();
			currentPlayer.Name = item.Id.ToString();
			currentPlayer.bombScene = bombScene;
			//currentPlayer.SetUpPlayer(item.Name);
			AddChild(currentPlayer);
			players.Add(currentPlayer);
			foreach (Node2D spawnPoint in GetTree().GetNodesInGroup("PlayerSpawnPoints"))
			{
				if(int.Parse(spawnPoint.Name) == index){
					currentPlayer.GlobalPosition = spawnPoint.GlobalPosition;
				}
			}
			index ++;
		}//
		
	}
	
	private void HandleBombPlacement()
	{
		//GD.Print("HandleBombPlacement called"); 

		foreach (var player in players)
		{
			string actionName = "place_bomb_p" + player.Name;
			GD.Print("Checking action: ", actionName);

			if (InputMap.HasAction(actionName) && Input.IsActionJustPressed(actionName) && player.canPlaceBomb)
			{
				GD.Print("Attempting to place bomb for player: " + player.Name);

				var bombInstance = (Bomb)bombScene.Instantiate();
				bombInstance.Position = player.Position;
				bombInstance.setFlameIncrease(player.nrOfRangeIncreasePowerUps);
				bombInstance.HasDetonated += player.OnBombHasDetonated;
				AddChild(bombInstance);

				player.canPlaceBomb = false;
				GD.Print("Bomb placed at: ", player.Position);
			}
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
		//GD.Print("Map1 _Process called");
		HandleBombPlacement();
	}
	//private void OnBombHasDetonated(){
		//playerInstance.canPlaceBomb = true;
	//}
}
