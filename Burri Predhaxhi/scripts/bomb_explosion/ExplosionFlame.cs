using Godot;
using System;
using System.Diagnostics;

public partial class ExplosionFlame : Node2D
{

	
	// Called when the node enters the scene tree for the first time.
	[Export]
	public int rangeIncrease = 0;

	public int X = 65;
	public int Y = 200;

	private const double flameAnimationIncrease = 1f / 3f;
	private const double lifetimeBoxlenRatio = 1.0 / 250.0;
	public float collisionPointDistance;

	public override void _Ready()
	{
		var collshape = GetNode<Area2D>("Area2D").GetNode<CollisionShape2D>("CollisionShape2D");


		RectangleShape2D rectangleShape = new RectangleShape2D
		{
			Size = new Vector2(X, Y + rangeIncrease*100)
		};

		collshape.Shape = rectangleShape;

		if (rangeIncrease > 0){
			 GetNode<GpuParticles2D>("Flame").Lifetime = (Y + rangeIncrease*100 + 50) * lifetimeBoxlenRatio ;
			 //GetNode<GpuParticles2D>("Flame").Lifetime += flameAnimationIncrease * rangeIncrease;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// if (!Resized){
			//ResizeFlame(collisionPoint);
			// Resized = true;
		// }


	}
	
	public override void _PhysicsProcess(double delta) 
	{	
		
	}
	
	public void ToggleFlameCollision(){
		var collshape = GetNode<Area2D>("Area2D").GetNode<CollisionShape2D>("CollisionShape2D");
		collshape.SetDeferred("disabled", !collshape.Disabled);
	}
	
	public void ToggleEmission(){
		var flame = GetNode<GpuParticles2D>("Flame");
		flame.SetDeferred("emitting", !flame.Emitting); 
	}
	
	private void OnBodyEntered(Node body)
	{	
		
		destroyObject(body);
		
	}


	private void OnArea2DEntered(Area2D area)
	{
		destroyObject(area);
	}
		

	private void destroyObject(Node body){
		if (body is IDestroyable){
			var destroyable = (IDestroyable) body;
			destroyable.Destroy();
		}
	}
	
	private double getFlameLifetime(float collisionPointDistance){
		return Math.Min(Y + rangeIncrease*100 + 50, collisionPointDistance) * lifetimeBoxlenRatio;
	}
	
	public void ResizeFlame(float collisionPointDistance){
		var collArea = GetNode<Area2D>("Area2D");
		var collshape = collArea.GetNode<CollisionShape2D>("CollisionShape2D");
		RectangleShape2D rectangleShape;
		var boxYPos = -Math.Min(collisionPointDistance / 2, (Y + rangeIncrease*100f) / 2);
		var collShapeYSize = Math.Min(collisionPointDistance, Y + rangeIncrease*100f + 50);

		var shapeSize = new Vector2(X, collShapeYSize);
		collArea.Position =  new Vector2(0, boxYPos);
		GetNode<GpuParticles2D>("Flame").Lifetime = getFlameLifetime(collisionPointDistance);
		var box = shapeSize; 
		rectangleShape = new RectangleShape2D
			{
				Size = box
			};
		collshape.Shape = rectangleShape;
	}

}



