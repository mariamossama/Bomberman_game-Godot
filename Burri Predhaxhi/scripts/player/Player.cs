using Godot;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

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

	
	//private const string _BombResource = "res://Bomb.tscn";
	PackedScene bombScene;
	
	
	public void Destroy() {
		GD.Print("Ocmuqinena");
		dead = true;
	}
	
	public override void _Ready()
	{
		//this.speed = 100;
		this.velocity = new Vector2();
		this.animationSprite = GetNode<Area2D>("PlayerArea2D").GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		bombScene = GD.Load<PackedScene>("res://Bomb.tscn");
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
	
	private void _on_Bomb_Detonated(Vector2 position){
		amountOfBombs++;
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
	private bool canPlaceBomb = true;
	private double bombPlacementCooldown = 0.5f; 
	private double timeSinceLastBomb = 0;

	public override void _Process(double delta)
	{
		if (canPlaceBomb && Input.IsActionJustPressed("place_bomb"))
		{
			GD.Print("space pressed for bomb placement");
			if (_TryPlaceBomb(Position)) {
				canPlaceBomb = false;
				timeSinceLastBomb = 0; 
			}
		}

		if (!canPlaceBomb) {
			timeSinceLastBomb += delta;
			if (timeSinceLastBomb >= bombPlacementCooldown) {
				canPlaceBomb = true;  
			}
		}
	}
		
	private void OnTreeExited()
	{
		EmitSignal(SignalName.PlayerWasRemoved);
	}
		protected bool _TryPlaceBomb(Vector2 bombposition)
	{
		if (amountOfBombs <= 0) { return false; };
		//if (isJustLoaded) { return false; };
		var bombInstance = (Node2D) bombScene.Instantiate();

		// Create a vector and assign x and y manually.
		Vector2 centeredPosition = new Vector2();
		centeredPosition.X = (float)(Math.Round(Position.X / 32) * 32);
		centeredPosition.Y = (float)(Math.Round(Position.Y / 32) * 32);
		
		List<Bomb> bombs = GetTree().Root.GetChildren().OfType<Bomb>().ToList();
		foreach (Bomb bomb in bombs)
		{
			if (bomb.Position == centeredPosition)
			{
				return false; //already a bomb
			}
		}
		bombInstance.Position =  centeredPosition ;
		GetTree().Root.AddChild(bombInstance);
		((Bomb)bombInstance).Detonated += _on_Bomb_Detonated;

		amountOfBombs--;
		return true;
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
	   amountOfBombs ++ ;
	
	}

	private void IncreaseFlameArea()
	{
		 flamePowerUp ++;
	}
}





