using Godot;
using System;

public partial class Box : RigidBody2D, IDestroyable
{

	//PowerUp inside ;
	private const string _PowerUpResourcetype1 = "res://Asset/BombIncreasePowerUp.tscn";
	private const string _PowerUpResourcetype2 = "res://Asset/FireRangeIncreasePowerUp.tscn";
	private PackedScene _ScenePowerUp;
	private PackedScene _ScenePowerUp2;
	
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
		_ScenePowerUp = ResourceLoader.Load<PackedScene>(_PowerUpResourcetype1);
		_ScenePowerUp2 = ResourceLoader.Load<PackedScene>(_PowerUpResourcetype2);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

public void OnBodyEntered(Node body)
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
		GameStateSingleton.FetchGameState().RayCastIgnores.Remove(this);
		GD.Print("Box destroyed");
		animatedSprite2D.Play("blown");
		collisionShape.SetDeferred("disabled", true);
		isDestroyed = true;
		collectPowerUps();
		QueueFree();
	}
	public bool getIsDestroyed()
	{
		return isDestroyed;
	}
	public bool gethasPowerUp()
	{
		return hasPowerUp;
	}
	public void setpowerUpChance(int value)
	{
		powerUpChance = value ;
	}
	public void collectPowerUps()
	{
		if (rng.Next(100) < powerUpChance)
				{
					hasPowerUp = true;
					SpawnPowerUp(Position);
				}
				else
				{
					GD.Print("No power-up spawned.");
				}

	}

	private void SpawnPowerUp(Vector2 boxPosition)
	 {
		// Randomly select the type of power-up
		PackedScene selectedPowerUpScene;
		if (rng.Next(2) == 0)
		{
			selectedPowerUpScene = _ScenePowerUp;
			GD.Print("Spawned Bomb Increase Power-Up.");
		}
		else
		{
			selectedPowerUpScene = _ScenePowerUp2;
			FireRangeIncreasePowerUp fr = (FireRangeIncreasePowerUp) selectedPowerUpScene.Instantiate();
			fr.Position = boxPosition; //temp
			AddChild(fr);
			GD.Print("Spawned Fire Range Increase Power-Up.");
		}

		
		
		//var bombInstance = (Bomb) bombScene.Instantiate();
		//newPowerUp.Position = boxPosition;
		//GetTree().Root.CallDeferred("add_child", newPowerUp);
	}
	

	public override void _Notification(int what)
	{
		// Check if the notification is for pre-deletion
		if (what == NotificationPredelete)
		{
	
			foreach (Node child in GetChildren())
			{
				if (child is Node2D)
				{
					((Node2D)child).Free();
				}
			}
		}
	}
	
	

}
