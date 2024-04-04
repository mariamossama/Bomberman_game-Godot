using Godot;
using System;

public partial class GameSettings2 : Control
{
	private OptionButton playerOptionButton;
	private LineEdit firstPlayerNameEdit;
	private LineEdit secondPlayerNameEdit;
	private bool isTwoPlayers;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		isTwoPlayers = false;
		playerOptionButton = GetNode<OptionButton>("OptionButton");
		firstPlayerNameEdit = GetNode<LineEdit>("LineEdit");
		secondPlayerNameEdit = GetNode<LineEdit>("LineEdit2");

		playerOptionButton.AddItem("One Player", 1);
		playerOptionButton.AddItem("Two Players", 2);

		// Use the observer pattern to deal with Signals (https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_signals.html)
		playerOptionButton.ItemSelected += (item) => this.OnPlayerOptionSelected(item);

		// Initially set to one player
		OnPlayerOptionSelected(1);
	}

	private void OnPlayerOptionSelected(long index)
	{
		// Toggle visibility of the second name input based on selection
		isTwoPlayers = index == 2;
		secondPlayerNameEdit.Visible = isTwoPlayers;
		
		// If only one player, disable the second input and clear it
		if (!isTwoPlayers)
		{
			secondPlayerNameEdit.Text = "";
		}
	}

	public void StartGame()
	{
		string firstPlayerName = firstPlayerNameEdit.Text;
		string secondPlayerName = isTwoPlayers ? secondPlayerNameEdit.Text : null;

		GD.Print("First Player's Name: " + firstPlayerName);
		if (isTwoPlayers)
		{
			GD.Print("Second Player's Name: " + secondPlayerName);
		}
		
		// Here you can initiate the game with one or two players
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
