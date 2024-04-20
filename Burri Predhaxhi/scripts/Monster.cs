using Godot;
using System;

public partial class Monster : CharacterBody2D, IDestroyable
{
	[Export]
	public int Speed { get; set; } = 100;
	private Vector2 velocity;
	private AnimatedSprite2D animationSprite;
	private Random random; //use GD.Randi() instead 
	private double timeSinceLastDirectionChange = 0f;
	private double directionChangeInterval = 1f; 
	public bool dead = false;

	public void Destroy() 
	{
		GD.Print("Monster destroyed");
		dead = true;
	}

	public override void _Ready()
	{
		animationSprite = GetNode<Area2D>("Area2D").GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		ChangeDirection(); // Begin with a random direction
	}

	public override void _PhysicsProcess(double delta)
	{
		if (dead) 
		{
			return; //??? 
		}

		//move in units

		timeSinceLastDirectionChange += delta;

		if (timeSinceLastDirectionChange >= directionChangeInterval)
		{
			timeSinceLastDirectionChange = 0f;
			ChangeDirection();
		}

		MoveAndCollide(velocity);
		ChangeAnimation(velocity);
	}

	private void ChangeDirection()
	{
		int randomDirection = random.Next(0, 4);
		switch (randomDirection)
		{
			case 0: velocity = new Vector2(1, 0); break; // Right
			case 1: velocity = new Vector2(-1, 0); break; // Left
			case 2: velocity = new Vector2(0, 1); break; // Down
			case 3: velocity = new Vector2(0, -1); break; // Up
		} // old + there are enums for directions

		Vector2[] directions = {Vector2.Up, Vector2.Down, Vector2.Left, Vector2.Right};

		//if coordinates both even: pick randomly from 4 directions
		// if x even, y odd, 2 directions: left, right
		// if x odd, y even, 2 direction: up, down

	}



	private void ChangeAnimation(Vector2 direction)
	{
		if (direction.X > 0)
		{	
			animationSprite.FlipH = true;
			animationSprite.Play("walk_side");
		}
		else if (direction.X < 0)
		{
			animationSprite.FlipH = false;
			animationSprite.Play("walk_side");
		}
		else if (direction.Y > 0)
		{
			animationSprite.Play("walk_down");
		}
		else if (direction.Y < 0)
		{
			animationSprite.Play("walk_up");
		}
	}

	private void OnAnimationFinished()
	{
		if (dead)
		{
			QueueFree();
		}
	}
}
