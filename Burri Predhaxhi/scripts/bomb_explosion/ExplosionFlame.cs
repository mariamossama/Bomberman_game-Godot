using Godot;
using System;

public partial class ExplosionFlame : Node2D
{

	
	// Called when the node enters the scene tree for the first time.
	[Export]
	public int rangeIncrease = 0;

	private const int X = 65;
	private const int Y = 268;

	private const double flameAnimationIncrease = 0.15;

	public override void _Ready()
	{
		var collshape = GetNode<Area2D>("Area2D").GetNode<CollisionShape2D>("CollisionShape2D");


		RectangleShape2D rectangleShape = new RectangleShape2D
		{
			Size = new Vector2(X, Y + rangeIncrease*100)
		};

		collshape.Shape = rectangleShape;

		if (rangeIncrease > 0){
			 GetNode<GpuParticles2D>("Flame").Lifetime += flameAnimationIncrease;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

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

}



