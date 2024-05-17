using Godot;
using System;
using System.IO;
using System.Linq;

public partial class Monster : CharacterBody2D, IDestroyable
{
	[Export]
	public float Speed { get; set; } = 5/3f; //NOTE: the monster moves in units, d/t where d is 50px (the size of
	//a tile scaled down in the level) whereas t can be chosen arbitrarily
	//currently some objects "knock" the user out of its unit of movement
	//TODO: address that (all objects must either be ignored or snapped to center and with size 50px)
	//or maybe we should see if there is any constraint that snaps the monster to the grid
	private Vector2 velocity;
	private AnimatedSprite2D animationSprite;
	public bool dead = false;

	RayCast2D up;
	RayCast2D down;
	RayCast2D left;
	RayCast2D right;
	Vector2 direction;
	public void Destroy() 
	{
		GD.Print("Monster destroyed");
		dead = true;
	}

	private Vector2[] getDirections(){
		return (new (Vector2, RayCast2D)[] {(Vector2.Up, up), (Vector2.Down, down), (Vector2.Left, left), (Vector2.Right, right)})
				.Where(x => !x.Item2.IsColliding()).Select(x=>x.Item1).ToArray();
	}

	public override void _Ready()
	{
		animationSprite = GetNode<Area2D>("MonsterArea").GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		direction = Vector2.Zero;
		up = GetNode<RayCast2D>("RayCastUp");
		down = GetNode<RayCast2D>("RayCastDown");
		left = GetNode<RayCast2D>("RayCastLeft");
		right = GetNode<RayCast2D>("RayCastRight");

		ChangeDirection(); // Begin with a random direction

		
	}

	public override void _PhysicsProcess(double delta)
	{

		velocity = direction * Speed;

		MoveAndCollide(velocity);
		ChangeAnimation(velocity);
	}

	private void ChangeDirection()
	{
		Random rnd = new Random();
		var viableDirections = getDirections();
		if (viableDirections.Length > 0)
			direction = viableDirections[rnd.Next(viableDirections.Length)];
		
	}
	
	private void OnDirectionChangeTimeout()
	{
		ChangeDirection();
	}

	private void ChangeAnimation(Vector2 direction)
	{
		if (dead){
			this.direction = Vector2.Zero;
			animationSprite.Play("die");
		} else if (direction.X > 0)
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
			GameStateSingleton.FetchGameState().RayCastIgnores.Remove(this);
			QueueFree();
		}
	}
}



