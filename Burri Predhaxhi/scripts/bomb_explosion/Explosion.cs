using Godot;
using System;

public partial class Explosion : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	
	public void ToggleFlames(){
		foreach (ExplosionFlame e in GetChildren()){
			e.ToggleFlameCollision();
			e.ToggleEmission();
		}
	}

	public void increaseChildrenRange(int value){
		foreach (ExplosionFlame e in GetChildren()){
			e.rangeIncrease = value;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
