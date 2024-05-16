using Godot;
using System;

public partial class FlameWallDetection : Node2D
{
	// Called when the node enters the scene tree for the first time.
	RayCast2D ray;

	public override void _Ready()
	{
		ray = GetNode<RayCast2D>("RayCast2D");

		foreach(CollisionObject2D ignoreNode in GameStateSingleton.FetchGameState().RayCastIgnores) {
			ray.AddException(ignoreNode);
		}
	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public override void _PhysicsProcess(double delta){

		var flame = GetNode<ExplosionFlame>("ExplosionFlame");
		
		flame.collisionPoint = ray.GetCollisionPoint();

	}
	
	public void ToggleFlame() {
		var flame = GetNode<ExplosionFlame>("ExplosionFlame");
		flame.ToggleFlameCollision();
		flame.ToggleEmission();
	}

	public void increaseFireRange(int value){
		var flame = GetNode<ExplosionFlame>("ExplosionFlame");
		flame.rangeIncrease = value;
	}
}
