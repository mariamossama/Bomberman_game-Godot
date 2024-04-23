using Godot;
using System;

public partial class PlayMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void _on_new_game_pressed()
	{
		
		var newgame = GD.Load<PackedScene>("res://menu/new_game_setting.tscn").Instantiate();
		GetTree().Root.AddChild(newgame);
		Hide();
	
	}
	private void _on_continue_pressed()
	{
		// Replace with function body.
	}
}






