using Godot;
using System;

public partial class Box : RigidBody2D, IDestroyable
{

	//PowerUp inside ;
	private const string _PowerUpResource = "res://Asset/BombIncreasePowerUp.tscn";
	private PackedScene _ScenePowerUp;
	
	private AnimatedSprite2D animatedSprite2D;
	 private CollisionShape2D collisionShape;
	 private Random rng = new Random();
	 private int powerUpChance = 50;
	 private bool isDestroyed = false;

	 private bool hasPowerUp = false ;
	 CharacterBody2D player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("BoxAnimation");
		collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
		_ScenePowerUp = ResourceLoader.Load<PackedScene>(_PowerUpResource);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

private void OnBodyEntered(Node body)
{
	if (!isDestroyed)
	{
		// Check if the node is a flame
		if (body is ExplosionFlame) 
		{
			GD.Print("Flame has hit the box, destroying box.");
			player = (CharacterBody2D)body;
			Destroy(); 
		}
		else
		{

			GD.Print("Box is still intact, cannot pass!");
		}
	}
}

	public void Destroy()
	{
		GD.Print("Box destroyed");
		animatedSprite2D.Play("blown");
		collisionShape.SetDeferred("disabled", true);
		isDestroyed = true;
		collectPowerUps();
	}

	private void collectPowerUps()
	{
		//why should boxes collect the powerups? 
		// if (rng.Next(100) < powerUpChance)
		// {
		// 	hasPowerUp = true;
		// 	SpawnPowerUp(Position);
		// 	GD.Print(Position);
		// 	var newPowerUp = (Area2D)  _ScenePowerUp.Instantiate();
		// 	newPowerUp.Position = Position;
		// 	// GetTree().Root.AddChild(newPowerUp);
		// 	GetTree().Root.CallDeferred("add_child", newPowerUp);
		// 	//QueueFree();
		// }
		// else
		// {
		// 	GD.Print("doesn't have a power up");
		// }

		// QueueFree();
	}

	private void SpawnPowerUp(Vector2 boxPosition)
	{
		// Randomly select the type of power-up
		//should get a method from the power up reference the box owns that gets the bonus and then assign the player reference the bonus 
		GD.Print("you got a pwer up : type ");
	
	}

}
