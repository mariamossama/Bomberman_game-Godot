using Godot;
using System;

public class Settings : Control
{
	private OptionButton playerOptionButton;
	private LineEdit firstPlayerNameEdit;
	private LineEdit secondPlayerNameEdit;

	public override void _Ready()
	{
		// Assign the nodes to the variables
		playerOptionButton = GetNode<OptionButton>("OptionButton");
		firstPlayerNameEdit = GetNode<LineEdit>("LineEdit");
		secondPlayerNameEdit = GetNode<LineEdit>("LineEdit2");

		// Add items to the OptionButton
		playerOptionButton.AddItem("One Player", 1);
		playerOptionButton.AddItem("Two Players", 2);

		// Connect the option selected signal to the method
		playerOptionButton.Connect("item_selected", this, nameof(OnPlayerOptionSelected));

		// Initially set to one player
		OnPlayerOptionSelected(1);
	}

	private void OnPlayerOptionSelected(int index)
	{
		// Toggle visibility of the second name input based on selection
		bool isTwoPlayers = index == 2;
		secondPlayerNameEdit.Visible = isTwoPlayers;
		
		// If only one player, disable the second input and clear it
		if (!isTwoPlayers)
		{
			secondPlayerNameEdit.Text = "";
		}
	}

	// This method can be connected to a "Start Game" button to use the names entered
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


