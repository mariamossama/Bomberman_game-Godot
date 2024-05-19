using Godot;
using System;

public partial class Bomb : RigidBody2D
{
	[Signal] 
	public delegate void HasDetonatedEventHandler(); 
	
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

	public void setFlameIncrease(int numberIncrease) {
		var explosion = GetNode<Explosion>("Explosion");
		explosion.increaseChildrenRange(numberIncrease);
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
		EmitSignal(SignalName.HasDetonated);
		explosion.ToggleFlames();
	}
	
	private void OnExplosionEndedTimerTimeout()
	{
		var explosion = GetNode<Explosion>("Explosion");
		explosion.ToggleFlames();
		GameStateSingleton.FetchGameState().RayCastIgnores.Remove(this);
		QueueFree();
	}

	private void OnBodyExited(Node2D body)
	{	
		if (body is IDestroyable){
			player = GetParent().GetNode<CharacterBody2D>("Player");
			RemoveCollisionExceptionWith(player);
		}
	}
	
}


