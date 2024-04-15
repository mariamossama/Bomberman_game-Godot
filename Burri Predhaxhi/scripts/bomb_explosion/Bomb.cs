using Godot;
using System;

public partial class Bomb : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.
	private AnimatedSprite2D animatedSprite2D;
	CharacterBody2D player;
	public override void _Ready()
	{
		//TODO Change this to generalize to many players
		player = GetParent().GetNode<CharacterBody2D>("Player");
		AddCollisionExceptionWith(player);
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

	private void OnBodyExited(Node2D body)
	{	
		GD.Print("hika");
		if (body is IDestroyable){
			GD.Print("dola");
			player = GetParent().GetNode<CharacterBody2D>("Player");
			RemoveCollisionExceptionWith(player);
		}
	}
}


