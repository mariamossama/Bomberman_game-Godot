using Godot;
using System;

public partial class new_game_setting : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void _on_start_pressed()
	{
		var newgame = GD.Load<PackedScene>("res://Gaming.tscn").Instantiate();
		GetTree().Root.AddChild(newgame);
		Hide();
	}
	private void map_1_pressed()
	{
		//var frame = GD.Load<PackedScene>("res://Gaming.tscn").Instantiate();
		//GetTree().Root.AddChild(frame);
		//var newgame = GD.Load<PackedScene>("res://Asset/Map/Map2.tscn").Instantiate();
		//GetTree().Root.AddChild(newgame);
		//Hide();// Replace with function body.
		var newgame = GD.Load<PackedScene>("res://menu/networking.tscn").Instantiate();
		GetTree().Root.AddChild(newgame);
		Hide();
		
	}
	
	private void map_2_pressed()
	{
		var frame = GD.Load<PackedScene>("res://Gaming.tscn").Instantiate();
		GetTree().Root.AddChild(frame);
		var newgame = GD.Load<PackedScene>("res://Asset/Map/Map3.tscn").Instantiate();
		GetTree().Root.AddChild(newgame);
		Hide();// Replace with function body.
	}
	
	private void map_3_pressed()
	{
		var frame = GD.Load<PackedScene>("res://Gaming.tscn").Instantiate();
		GetTree().Root.AddChild(frame);
		var newgame = GD.Load<PackedScene>("res://Asset/Map/Map1.tscn").Instantiate();
		GetTree().Root.AddChild(newgame);
		Hide();// Replace with function body.
	}
}





