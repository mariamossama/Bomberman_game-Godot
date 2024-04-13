using Godot;
using System;

public partial class Level : Node2D
{
	PackedScene bombScene;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
	
}



