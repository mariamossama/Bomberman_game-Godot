using Godot;
using System;

public partial class ExplosionFlame : Node2D
{

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//if (currentDistance > distance) {
			//QueueFree();
		//}
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
		if (body is IDestroyable){
			var destroyable = (IDestroyable) body;
			destroyable.Destroy();
		}
		
		
	}
	

}

