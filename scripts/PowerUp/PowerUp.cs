using Godot;
using System;
using System.Collections.Generic;

public partial class PowerUp : Area2D
{
	private List<string> _PowerUpTextures = new List<string>{
		"res://bombman/item_flame.png",
		"res://bombman/item_multibomb.png",
	 };
	private List<string> _PowerUpNames = new List<string>{
		"Powerup_flame",
		"Powerup_bomb",
		
		 };
	private bool _isInvincible = true;
	private Sprite2D _sprite;
	private bool _isFlickerOn = true;
	private bool _isShouldFlicker = false;

	public string typeOfPowerUp; 
		
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_sprite = (Sprite2D)this.GetNode("./Sprite2D");
		// Set random PowerUp type and texture
		Random random = new Random();
		int index = random.Next(_PowerUpTextures.Count);
		typeOfPowerUp = _PowerUpNames[index];
		Texture img = (Texture2D)GD.Load(_PowerUpTextures[index]);
		_sprite.Texture = (Texture2D)img;
		// Setup timers
		var expireTimer = GetNode<Timer>("Expire");
		var timer_about_to_expire = GetNode<Timer>("ExpireAboutTo");
		var timer_invincibility = GetNode<Timer>("Invincibility");
		var timer_flicker = GetNode<Timer>("Flicker");
		
	}

	//async public void name()
	//GetNode<Timer>("MessageTimer").Start();//var messageTimer = GetNode<Timer>("MessageTimer");//await ToSignal(messageTimer, Timer.SignalName.Timeout);
	//await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);//GetNode<Label>("Message").Hide();
	public override void _PhysicsProcess(double delta)
	{
		var overlappingAreas = GetOverlappingAreas();
		if (overlappingAreas.Count > 0){
			
			foreach(Area2D area in overlappingAreas){
				if (area.Name.ToString().StartsWith("Player")){
					PlayerPickup();
				}
				else {
					Ignite();
				}
			}
			Ignite();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		
	}
	private void Expire()
	{
		 QueueFree();
	}
		
	private void OnExpireTimeout()
	{
		 QueueFree();
	}

	private void TurnOnFlickerCloseToExpiration()
	{
		_isShouldFlicker = true;
	}

	private void PlayerPickup(){
		Expire();
	}

	private void Ignite(){
		if ( _isInvincible ){
			return;
		}
		Expire();
	}
	private void Flicker()
	{
		if (_isShouldFlicker)
		{
			if (_isFlickerOn)
			{
				_sprite.Modulate = new Color(1, 1, 1, 0.3f);
				_isFlickerOn = false;
			}
			else
			{
				_sprite.Modulate = new Color(1, 1, 1, 1f);
				_isFlickerOn = true;
			}
		}
		else
		{
			if (!_isFlickerOn)
			{
				_isFlickerOn = true;
				_sprite.Modulate = new Color(1, 1, 1, 1f);
			}
		}
	}


	private void RemoveInvincibility()
	{
		_isInvincible = false;
	}
}







