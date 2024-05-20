using Godot;
using System;

public partial class GamingInfo : Node
{
	private Control _pausePanel;
	private Control _exitMenu;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_pausePanel = GetNode<Control>("PausePanel");
		_pausePanel.Hide();
		_exitMenu = GetNode<Control>("ExitWindow");
		_exitMenu.Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void _on_exit_pressed()
	{
		_exitMenu.Show();
	}
	private void _pause()
	{
		GetTree().Paused = true;
		_pausePanel.Show();
	}
	private void _unpause()
	{
		GetTree().Paused = false;
		_pausePanel.Hide();
	}

	
	private void starter()
	{
		_exitMenu.Hide();
		QueueFree();
		var game = GD.Load<PackedScene>("res://Menu.tscn").Instantiate();
		GetTree().Root.AddChild(game);
		
	}
	private void _on_yes_pressed()
	{
		GD.Print("quit");
		GetTree().Quit();
	}
	private void _on_no_pressed()
	{
		_exitMenu.Hide();
	}	
	
	private void _on_custom_setting()
	{
		var game = GD.Load<PackedScene>("res://menu/custsetting.tscn").Instantiate();
		GetTree().Root.AddChild(game);
		// Replace with function body.
	}
}






