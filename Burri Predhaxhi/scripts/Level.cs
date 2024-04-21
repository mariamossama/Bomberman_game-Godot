using Godot;
using System;
using System.Linq;

public partial class Level : Node2D
{
	PackedScene bombScene;
	PackedScene playerScene;

	PackedScene monsterScene;
	private int numOfPlayers;

	TileMap tileMap;
	Vector2I initialPlayerPos; //FIXME: should be an array of initial positions (3 positions)

	public override void _Ready()
	{
		initialPlayerPos = new Vector2I(-5,-5);
		//TODO: the number of players shouldn't be hardcoded but be determined by the menu
		numOfPlayers = 1;
		bombScene = GD.Load<PackedScene>( "res://Bomb.tscn");
		playerScene = GD.Load<PackedScene>("res://Player.tscn");
		monsterScene = GD.Load<PackedScene>("res://Monster.tscn");
		tileMap = GetNode<Node2D>("LevelFloor").GetNode<TileMap>("TileMap");
		SpawnPlayers();
		SpawnMonsters();
	}

	public override void _Process(double delta)
	{

		//if (Input.IsActionJustPressed("place_bomb")){
			//var bombInstance = (Node2D) bombScene.Instantiate();
			//var playerPos = GetNode<CharacterBody2D>("Player").Position;
			//
			//bombInstance.Position = playerPos;
			//AddChild(bombInstance);
		//}

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
	
	private void SpawnMonsters(){ //generalize for multiple monsters (create multiple instances)
		//available coordinates for spawning: floor tiles with x > -1, y > -2
		//decided arbitrarily so that monsters do not spawn very close to the player
		//atlas id for floor tiles: (0,4)
		int XBound = -1;
		int YBound = -2;
		var availableTiles = tileMap.GetUsedCellsById(0, -1, new Vector2I(0,4))
				.Where(vec => vec.X > XBound && vec.Y > YBound).ToList<Vector2I>();

		int randomIndex = (int) GD.Randi() % availableTiles.Count;
		Vector2I randomAvailCoordinate = availableTiles[randomIndex];
		var monsterInstance = (Monster) monsterScene.Instantiate();
		monsterInstance.Position = ToLocal(convertedCoords(randomAvailCoordinate));

		AddChild(monsterInstance);
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





