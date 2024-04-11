using Godot;
using System;

public partial class BombExplosion : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Node2D>("Explosion").Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void OnExplosionStartTimerTimeout()
	{
		GetNode<Node2D>("Explosion").Visible = true;
		//var nodePathToDisabledProperty = GetNode<Node2D>("Explosion").GetNode<Node2D>("ExplosionFlame").GetNode<RigidBody2D>("RigitBody2D").GetNode<CollisionShape2D>("CollisionShape2D");
		//nodePathToDisabledProperty.Disabled = false;
		
	}

}


