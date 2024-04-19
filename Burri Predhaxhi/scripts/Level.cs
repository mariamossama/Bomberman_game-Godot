using Godot;
using System;

public partial class Level : Node2D
{
	PackedScene bombScene;
	PackedScene playerScene;
	private int numOfPlayers;

	TileMap tileMap;
	Vector2I initialPlayerPos;

	public override void _Ready()
	{
		initialPlayerPos = new Vector2I(-5,-5);
		//TODO: the number of players shouldn't be hardcoded but be determined by the menu
		numOfPlayers = 1;
		bombScene = GD.Load<PackedScene>( "res://Bomb.tscn");
		playerScene = GD.Load<PackedScene>("res://Player.tscn"); //to be used in respawn
		tileMap = GetNode<Node2D>("LevelFloor").GetNode<TileMap>("TileMap");
		SpawnPlayers();
	}

	public override void _Process(double delta)
	{

		if (Input.IsActionJustPressed("place_bomb")){
			var bombInstance = (Node2D) bombScene.Instantiate();
			var playerPos = GetNode<CharacterBody2D>("Player").Position;
			
			bombInstance.Position = playerPos;
			AddChild(bombInstance);
		}

	}

	public Vector2 convertedCoords(int x,int y){ //overloaded for ease of use 
		return tileMap.ToGlobal(tileMap.MapToLocal(new Vector2I(x, y))); //in case you like using coordinates as ints more
	}

	public Vector2 convertedCoords(Vector2I pos){
		return tileMap.ToGlobal(tileMap.MapToLocal(pos));
	}

	private void SpawnPlayers(){ //FIXME: currently just 1 player
		var playerInstance = (Player) playerScene.Instantiate();
		GD.Print(ToLocal(convertedCoords(initialPlayerPos)));
		playerInstance.PlayerWasRemoved += OnPlayerWasRemoved;
		playerInstance.Position = ToLocal(convertedCoords(initialPlayerPos)); //????
		AddChild(playerInstance);
	}
	
	private void SpawnMonsters(){

	}
	
	private void OnPlayerWasRemoved()
	{
		numOfPlayers--;
		if (numOfPlayers == 0){
			SpawnPlayers();
			SpawnMonsters();
		}
	}
	
}





