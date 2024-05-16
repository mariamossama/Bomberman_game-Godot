using Godot;
using System;

public partial class Explosion : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	
	public void ToggleFlames(){
		foreach (FlameWallDetection e in GetChildren()){
			e.ToggleFlame();
		}
	}

	public void increaseChildrenRange(int value){
		foreach (FlameWallDetection e in GetChildren()){
			e.increaseFireRange(value);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
