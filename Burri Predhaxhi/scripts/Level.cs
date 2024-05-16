using Godot;
using System;
using System.Linq;

public partial class Level : Node2D
{
	PackedScene bombScene;
	PackedScene playerScene;
	PackedScene powerUpScene;

	PackedScene monsterScene;
	private int numOfPlayers;


	TileMap tileMap;
	public Utils utils;

	Player playerInstance; //change to list when generalizing to more than 1 player

	private int bombCount = 0;

	public override void _Ready()
	{
		//TODO: the number of players shouldn't be hardcoded but be determined by the menu
		numOfPlayers = 1;
		bombScene = GD.Load<PackedScene>( "res://Bomb.tscn");
		playerScene = GD.Load<PackedScene>("res://Player.tscn");
		monsterScene = GD.Load<PackedScene>("res://Monster.tscn");
		tileMap = GetNode<Node2D>("LevelFloor").GetNode<TileMap>("TileMap");
		utils = new Utils(tileMap);
		foreach (RigidBody2D box in GetChildren().Where(x => x is Box)) {
			GameStateSingleton.FetchGameState().RayCastIgnores.Add(box);
		}
		SpawnPlayers();
		SpawnMonsters();
		InsertPowerUps();
	}

	public override void _Process(double delta)
	{
		PlaceBomb();
	}

	private void PlaceBomb(){

		if (Input.IsActionJustPressed("place_bomb_p1") && playerInstance.canPlaceBomb){
			GD.Print("Bfore instantioation");
			var bombInstance = (Bomb) bombScene.Instantiate();
			GD.Print("Aftor instantioation");
			var playerPos = GetNode<CharacterBody2D>("Player").Position;
			
			bombInstance.setFlameIncrease(playerInstance.nrOfRangeIncreasePowerUps);
			bombInstance.Position = playerPos;
			bombInstance.HasDetonated += OnBombHasDetonated;
			AddChild(bombInstance);
			bombCount++;
			GD.Print("current bomb count vs max: " + bombCount + " , " + playerInstance.maxBombCount);

			if (bombCount == playerInstance.maxBombCount){
				playerInstance.canPlaceBomb = false;
				bombCount = 0;
			}

			GameStateSingleton.FetchGameState().RayCastIgnores.Add(bombInstance);
		}
	}

	private void SpawnPlayers(){ //FIXME: currently just 1 player
		playerInstance = (Player) playerScene.Instantiate();
		playerInstance.initialPlayerPos = new Vector2I(-5,-5); //FIXME: to be turned to a list likely
		GD.Print(ToLocal(utils.convertedCoords(playerInstance.initialPlayerPos)));
		playerInstance.PlayerWasRemoved += OnPlayerWasRemoved;
		playerInstance.Position = ToLocal(utils.convertedCoords(playerInstance.initialPlayerPos)); //????
		AddChild(playerInstance);
		GameStateSingleton.FetchGameState().RayCastIgnores.Add(playerInstance);
	}
	
	private void SpawnMonsters(){ //generalize for multiple monsters (create multiple instances)
		//available coordinates for spawning: floor tiles with x > -1, y > -2
		//decided arbitrarily so that monsters do not spawn very close to the player
		//atlas id for floor tiles: (0,4)
		int XBound = -1;
		int YBound = -2;
		var availableTiles = tileMap.GetUsedCellsById(0, -1, new Vector2I(0,4))
				.Where(vec => vec.X > XBound && vec.Y > YBound).ToList<Vector2I>();
	
		Vector2I randomAvailCoordinate = availableTiles[Convert.ToInt32(GD.Randi() % availableTiles.Count)]; 
		
		var monsterInstance = (Monster) monsterScene.Instantiate();
		monsterInstance.Position = ToLocal(utils.convertedCoords(randomAvailCoordinate));

		AddChild(monsterInstance);
		GameStateSingleton.FetchGameState().RayCastIgnores.Add(monsterInstance);
	}
	
	private void OnPlayerWasRemoved() //FIXME: misleading name, add a restart method and queuefree monsters as well
	{
		numOfPlayers--;
		if (numOfPlayers == 0){
			SpawnPlayers();
			SpawnMonsters(); 
		}
	}

	private void OnBombHasDetonated(){
		playerInstance.canPlaceBomb = true;
	}


	private void InsertPowerUps(){
		powerUpScene = GD.Load<PackedScene>( "res://Asset/FireRangeIncreasePowerUp.tscn");
		FireRangeIncreasePowerUp fr = (FireRangeIncreasePowerUp) powerUpScene.Instantiate();
		fr.Position = new Vector2(426, 77); //temp
		AddChild(fr);
	}

	
	
}


