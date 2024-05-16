using Godot;
using System;

public partial class Player : CharacterBody2D, IDestroyable
{
	[Export]
	public int Speed { get; set; } = 100;
	public bool dead = false;

	private Vector2 velocity;
	private AnimatedSprite2D animationSprite;

	 [Signal]
	 public delegate void PlayerWasRemovedEventHandler();

	 Vector2 direction;

	public int maxBombCount = 1; //initially one, when you get the bomb number increasing powerup, make sure to increment it
	public bool canPlaceBomb = true;

	public int nrOfRangeIncreasePowerUps = 0;

	public Vector2I initialPlayerPos;
	
	public void Destroy() {
		GD.Print("Ocmuqinena");
		dead = true;
	}
	
	public override void _Ready()
	{
		this.velocity = new Vector2();
		this.direction = Vector2.Zero;
		this.animationSprite = GetNode<Area2D>("PlayerArea2D").GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}
	
	public override void _PhysicsProcess(double _delta) {
		if (!dead){
			direction = new Vector2(
				Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"),
				Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up")
			); //when generalizing to multiple players, these keys must be supplied in level
			
			if (direction.Length() > 1) {
				direction = direction.Normalized();
			}
		}
		Velocity = direction * Speed;
		changeAnimation(direction);
		MoveAndSlide();

		DieAfterCollidingWMonster();
	}
	
	private void changeAnimation(Vector2 direction) {
		if (dead) {
			animationSprite.Play("die");
		}
		else if (direction == Vector2.Right) {
			animationSprite.Play("walk_right");
		} else if (direction == Vector2.Left) {
			animationSprite.Play("walk_left");
		} else if (direction == Vector2.Up) {
			animationSprite.Play("walk_up");
		} else if (direction == Vector2.Down) {
			animationSprite.Play("walk_down");
		} else if (direction == Vector2.Zero) {
			StringName karin = animationSprite.Animation;
			String k = (String) karin;
			animationSprite.Play(k.Replace("walk","idle"));
		}

	}

	private void OnAnimationFinished(){
		if (dead){
			GameStateSingleton.FetchGameState().RayCastIgnores.Remove(this);
			QueueFree();
		}
	}
	
	public override void _Process(double delta)
	{

	}
	
	private void OnTreeExited()
	{
		EmitSignal(SignalName.PlayerWasRemoved);
	}

	private void DieAfterCollidingWMonster(){
		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			KinematicCollision2D collision = GetSlideCollision(i);
			if (collision.GetCollider() is Monster)
				dead = true;
		}
	}


}
