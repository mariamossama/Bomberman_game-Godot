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
	private const string SaveFilePath = "res://menu/key_settings.cfg";
	public String controls;
	public int maxBombCount = 1; //initially one, when you get the bomb number increasing powerup, make sure to increment it
	public bool canPlaceBomb = true;
	public PackedScene bombScene;

	public int nrOfRangeIncreasePowerUps = 0;

	public Vector2I initialPlayerPos;
	
	public void Destroy() {
		GD.Print("Ocmuqinena");
		dead = true;
	}
	
	public override void _Ready()
	{
		//this.speed = 100;
		GetNode<MultiplayerSynchronizer>("MultiplayerSynchronizer").SetMultiplayerAuthority(int.Parse(Name));
		this.velocity = new Vector2();
		this.direction = Vector2.Zero;
		this.animationSprite = GetNode<Area2D>("PlayerArea2D").GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		//bombScene = GD.Load<PackedScene>("res://Bomb.tscn");
	}
	//private void PlaceBomb()
	//{
		//if (Input.IsActionJustPressed("place_bomb") && canPlaceBomb)
		//{
			//GD.Print("Attempting to place bomb");
			//var bombInstance = (Bomb)bombScene.Instantiate();
			//bombInstance.Position = Position;
			//bombInstance.setFlameIncrease(nrOfRangeIncreasePowerUps);
			//bombInstance.HasDetonated += OnBombHasDetonated;
			//GetParent().AddChild(bombInstance);
//
			//canPlaceBomb = false; 
			//GD.Print("Bomb placed at: ", Position);
		//}
	//}
	public void OnBombHasDetonated()
	{
		canPlaceBomb = true;
		//GD.Print("Bomb detonated");
	}

	public String GetControls()
	{
		var configFile = new ConfigFile();
		var err = configFile.Load(SaveFilePath);

		if (err == Error.Ok)
		{
			controls = configFile.GetValue("Controls", "LastButtonPressed", "None").ToString();
			GD.Print($"Last button pressed: {controls}");
			return controls;
		}
		else
		{
			GD.Print($"Failed to load config: {err}");
		}
		return "";
	}
	public override void _PhysicsProcess(double _delta) {
		//if(GetNode<MultiplayerSynchronizer>("MultiplayerSynchronizer").GetMultiplayerAuthority() == Multiplayer.GetUniqueId()){
		
		if (GetNode<MultiplayerSynchronizer>("MultiplayerSynchronizer").GetMultiplayerAuthority() == Multiplayer.GetUniqueId())
		{
			GD.Print("Has authority"); 
			if (!dead)
			{
				if(GetControls() == "ArrowKeys")
				{
				direction = new Vector2(
					Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left"),
					Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up")
				);
				}
				else
				{
					direction = new Vector2(
					Input.GetActionStrength("right_optionwasd") - Input.GetActionStrength("left_optionwasd"),
					Input.GetActionStrength("down_optionwasd") - Input.GetActionStrength("up_optionwasd")
				);
				}
				//GD.Print("Direction raw: ", direction); 

				if (direction.Length() > 1)
				{
					direction = direction.Normalized();
				}

				//GD.Print("Direction normalized: ", direction); 

				velocity = direction * Speed;
				//GD.Print("Velocity: ", velocity);
					
				Velocity = velocity;
				//GD.Print("Updated Velocity: ", Velocity); 

				// Ensure MoveAndSlide is called with the correct velocity
				MoveAndSlide();
				DieAfterCollidingWMonster();
				
				changeAnimation(direction);
			}
		}
	}
	private void changeAnimation(Vector2 direction)
	{
		GD.Print("changeAnimation called with direction: ", direction); 
		if (dead)
		{
			animationSprite.Play("die");
			//GD.Print("Animation: die");
		}
		else if (direction == Vector2.Right)
		{
			animationSprite.Play("walk_right");
			//GD.Print("Animation: walk_right");
		}
		else if (direction == Vector2.Left)
		{
			animationSprite.Play("walk_left");
			//GD.Print("Animation: walk_left");
		}
		else if (direction == Vector2.Up)
		{
			animationSprite.Play("walk_up");
			//GD.Print("Animation: walk_up");
		}
		else if (direction == Vector2.Down)
		{
			animationSprite.Play("walk_down");
			//GD.Print("Animation: walk_down");
		}
		else if (direction == Vector2.Zero)
		{
			StringName karin = animationSprite.Animation;
			String k = (String) karin;
			animationSprite.Play(k.Replace("walk", "idle"));
			//GD.Print("Animation: idle");
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
	// 	protected bool _TryPlaceBomb(Vector2 bombposition)
	// {
	// 	if (amountOfBombs <= 0) { return false; };
	// 	//if (isJustLoaded) { return false; };
	// 	var bombInstance = (Node2D) bombScene.Instantiate();

	// 	// Create a vector and assign x and y manually.
	// 	Vector2 centeredPosition = new Vector2();
	// 	centeredPosition.X = (float)(Math.Round(Position.X / 32) * 32);
	// 	centeredPosition.Y = (float)(Math.Round(Position.Y / 32) * 32);
		
	// 	List<Bomb> bombs = GetTree().Root.GetChildren().OfType<Bomb>().ToList();
	// 	foreach (Bomb bomb in bombs)
	// 	{
	// 		if (bomb.Position == centeredPosition)
	// 		{
	// 			return false; //already a bomb
	// 		}
	// 	}
	// 	bombInstance.Position =  centeredPosition ;
	// 	GetTree().Root.AddChild(bombInstance);
	// 	((Bomb)bombInstance).Detonated += _on_Bomb_Detonated;

	// 	amountOfBombs--;
	// 	return true;
	// }
	private void DieAfterCollidingWMonster(){
		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			KinematicCollision2D collision = GetSlideCollision(i);
			if (collision.GetCollider() is Monster)
				dead = true;
		}
	}
	

}
