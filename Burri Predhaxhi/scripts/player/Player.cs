using Godot;
using System;

public partial class Player : CharacterBody2D, IDestroyable
{
	[Export]
	public int Speed { get; set; } = 100;
	public bool dead = false;

	private Vector2 velocity;
	private AnimatedSprite2D animationSprite;
	
	public void Destroy() {
		GD.Print("Ocmuqinena");
		dead = true;
	}
	
	public override void _Ready()
	{
		//this.speed = 100;
		this.velocity = new Vector2();
		this.animationSprite = GetNode<Area2D>("Area2D").GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}
	
	public override void _PhysicsProcess(double _delta) {
		Vector2 direction = new Vector2(
			Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"),
			Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up")
		);
		
		if (direction.Length() > 1) {
			direction = direction.Normalized();
		}

		Velocity = direction * Speed;
		changeAnimation(direction);
		MoveAndSlide();
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
			QueueFree();
			//TODO: write a restart method
		}
	}
	
	// private void PlaceBomb(){
	// 	var bomb = new Bomb();
	// 	var p = Position;
	// 	bomb.Position = p;
	// 	GetParent().AddChild(bomb);
	// }
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	
	// public override void _Input(InputEvent @event)
	// {	
	// 	if (@event.IsActionPressed("place_bomb"))
  	// 	{
	// 		PlaceBomb();
	// 	}
	// }
}
