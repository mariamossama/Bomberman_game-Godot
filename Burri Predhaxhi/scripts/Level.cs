using Godot;
using System;

public partial class Level : Node2D
{
	PackedScene bombScene;
	private int numOfPlayers;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//TODO: the number of players shouldn't be hardcoded but be determined by the menu
		numOfPlayers = 1;
		bombScene = GD.Load<PackedScene>( "res://Bomb.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		if (Input.IsActionJustPressed("place_bomb")){
			var bombInstance = (Node2D) bombScene.Instantiate();
			var playerPos = GetNode<CharacterBody2D>("Player").Position;
			
			bombInstance.Position = playerPos;
			AddChild(bombInstance);
		}

		


	}

	public void RestartGame(){
		GetTree().ReloadCurrentScene(); //FIXME
	}
	
	
	private void OnPlayerWasRemoved()
	{
		numOfPlayers--;
		if (numOfPlayers == 0){
			RestartGame();
		}
	}
	
}





