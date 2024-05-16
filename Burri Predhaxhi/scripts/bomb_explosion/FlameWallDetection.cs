using Godot;
using System;
using System.Linq;

public partial class FlameWallDetection : Node2D
{
	// Called when the node enters the scene tree for the first time.
	RayCast2D ray;
	bool RayHitSomething = false;

	public override void _Ready()
	{
		ray = GetNode<RayCast2D>("RayCast2D");
		var ignores = GameStateSingleton.FetchGameState().RayCastIgnores;
		GD.Print(ignores);
		foreach(CollisionObject2D ignoreNode in ignores) {
			ray.AddException(ignoreNode);
		}
	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public override void _PhysicsProcess(double delta){
		var flame = GetNode<ExplosionFlame>("ExplosionFlame");
		if (ray.IsColliding()) {
			flame.ResizeFlame(ToLocal(ray.GetCollisionPoint()).Length());
			RayHitSomething = true;
		}
		if (!RayHitSomething)
			flame.ResizeFlame(99999999);
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

	// public void ResizeFlame(Vector2 collisionPoint){
	// 	var flame = GetNode<ExplosionFlame>("ExplosionFlame");
	// 	var collArea = flame.GetNode<Area2D>("Area2D");
	// 	var collshape = collArea.GetNode<CollisionShape2D>("CollisionShape2D");
		

	// 	RectangleShape2D rectangleShape;

	// 	var collPoint = ToLocal(collisionPoint);
	// 	var maxBoxY = new Vector2(flame.X, flame.Y + flame.rangeIncrease*50);
	// 	collArea.Position = new Vector2(collArea.Position.X , (collPoint.Y) / 2);
		
	// 	var box = maxBoxY > new Vector2(flame.X, Math.Abs(collPoint.Y)) ? new Vector2(flame.X, Math.Abs(collPoint.Y)) : maxBoxY ; 
	// 	rectangleShape = new RectangleShape2D
	// 		{
	// 			Size = box
	// 		};
	// 	collshape.Shape = rectangleShape;
	// }
}
