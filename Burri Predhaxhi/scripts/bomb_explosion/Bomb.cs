using Godot;
using System;

public partial class Bomb : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.
	private AnimatedSprite2D animatedSprite2D;
	public override void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("BombAnimation");
		animatedSprite2D.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void OnBombTimerTimeout()
	{
		//QueueFree();
		animatedSprite2D.Hide();
	}

	private void OnExplosionStartTimerTimeout()
	{
		var explosion = GetNode<Explosion>("Explosion");
		explosion.ToggleFlames();
	}
	
	private void OnExplosionEndedTimerTimeout()
	{
		var explosion = GetNode<Explosion>("Explosion");
		explosion.ToggleFlames();
		QueueFree();
	}
}
