using Godot;
using System;

public partial class PlayerArea2D : Area2D
{
	[Signal]
	 public delegate void PickedUpPowerUpEventHandler(string typeOfPowerUp);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public override void _PhysicsProcess(double delta)
	{
		var overlappingAreas = GetOverlappingAreas();
		
		if (overlappingAreas.Count > 0){
			foreach(Area2D area in overlappingAreas){
				if (area is PowerUp){
					string typeOfPowerUp = ((PowerUp)area).typeOfPowerUp;
					EmitSignal(nameof(PickedUpPowerUp), typeOfPowerUp);
				}
				
			}
		}
	}
}
