using Godot;
using System;

public partial class Player : CharacterBody2D, IDestroyable
{
	[Signal]
	 public delegate void PlayerWasRemovedEventHandler();

	//Player Property
	[Export]
	public int Speed { get; set; } = 100;
	public bool dead = false;
	//public int BombNum = 1;
	//public int FlameSize = 1;
	
	//powerup
	public int amountOfBombs = 1;
	public int bombPowerUp = 0;
	public int flamePowerUp = 0;
	
	private const string _BombResource = "res://Nodes/Bomb.tscn";
	private PackedScene _packedSceneBomb;
	
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
		this.animationSprite = GetNode<Area2D>("PlayerArea2D").GetNode<AnimatedSprite2D>("AnimatedSprite2D");
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
		
		if (Input.IsActionPressed("place_bomb"))
		{
			_TryPlaceBomb();
		}
	}
		// TODO:Send signal to game manager to spawn bombs
	protected bool _TryPlaceBomb()
	{
		//if (amountOfBombs <= 0) { return false; };
		//if (isJustLoaded) { return false; };
		//Bomb newBomb = _packedSceneBomb.Instance() as Bomb;
		//newBomb.Init(1 + (flamePowerUp * flamePowerUpValue));
		//// Change position to center
		//Vector2 centeredPosition = new Vector2();
		//centeredPosition.x = (float)(Math.Round(Position.x / 32) * 32);
		//centeredPosition.y = (float)(Math.Round(Position.y / 32) * 32);
		//// Check if there already is a Bomb on center position
		//List<Bomb> bombs = GetTree().Root.GetChildren().OfType<Bomb>().ToList();
		//foreach (Bomb bomb in bombs)
		//{
			//if (bomb.Position == centeredPosition)
			//{
				//return false;
			//}
		//}
		//newBomb.Position = centeredPosition;
		//GetTree().Root.AddChild(newBomb);
		//newBomb.Connect("Detonated", this, "_on_Bomb_Detonated");
		amountOfBombs--;
		return true;
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
			//TODO: write a restart method, logic will change when there are more players
			
		}
	}
	
	public override void _Process(double delta)
	{

	}
	
	private void OnTreeExited()
	{
		EmitSignal(SignalName.PlayerWasRemoved);
	}
	private void pick_up_power_up(string typeOfPowerUp)
	{
		if (typeOfPowerUp == "Powerup_bomb")
		{
			IncreaseBombNum();
		}
		else if (typeOfPowerUp == "Powerup_flame")
		{
			IncreaseFlameArea();
		}
	}
	private void IncreaseBombNum()
	{
	   
	}

	private void IncreaseFlameArea()
	{
		
	}
	
}





