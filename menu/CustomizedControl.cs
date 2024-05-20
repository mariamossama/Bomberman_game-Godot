using Godot;
using System;

public partial class CustomizedControl : Control
{
	public String settings ;
	private const string SaveFilePath = "res://menu/key_settings.cfg";

	public override void _Ready()
	{
		GD.Print("Configuration file path: ");
		LoadSettings();
	}

	private void SaveButtonPress(string buttonName)
	{ 
		
			settings = buttonName;
			GD.Print($"Button {settings} was pressed and saved to config.");
			var configFile = new ConfigFile();
		configFile.SetValue("Controls", "LastButtonPressed", settings);
		Error err = configFile.Save(SaveFilePath);

		if (err == Error.Ok)
		{
			GD.Print($"Button {settings} was pressed and saved to config.");
		}
		else
		{
			GD.Print($"Failed to save config: {err}");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_wasd_pressed()
	{
		SaveButtonPress("WASD");
	}


	private void _on_arrows_pressed()
	{
		SaveButtonPress("ArrowKeys");
	}
	
	 private void LoadSettings()
	{
		var configFile = new ConfigFile();
		var err = configFile.Load(SaveFilePath);

		if (err == Error.Ok)
		{
			settings = configFile.GetValue("Controls", "LastButtonPressed", "None").ToString();
			GD.Print($"Last button pressed: {settings}");
		}
		else
		{
			GD.Print($"Failed to load config: {err}");
		}
	}
}



