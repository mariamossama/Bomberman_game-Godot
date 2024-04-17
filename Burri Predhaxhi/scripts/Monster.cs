using Godot;
using System;

public partial class Monster : CharacterBody2D, IDestroyable
{
	[Export]
	public int Speed { get; set; } = 100;
	private Vector2 velocity;
	private AnimatedSprite2D animationSprite;
	private Random random = new Random();
	private double timeSinceLastDirectionChange = 0f;
	private double directionChangeInterval = 1f; // Time in seconds to change direction
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
			// If the monster is dead, we don't want it to keep moving.
			return;
		}

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
		}
	}

	private void ChangeAnimation(Vector2 direction)
	{
		if (direction.X > 0)
		{
			animationSprite.Play("walk_right");
		}
		else if (direction.X < 0)
		{
			animationSprite.Play("walk_left");
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
			QueueFree(); // Remove the monster from the scene
			// Optionally, trigger a respawn or level reset here
		}
	}
}
