//using Godot;
//using System;
//using System.Collections.Generic;
//
//public partial class custsetting : Control
//{
	//private Dictionary<string, string> _keyMappings;
	//private const string SaveFilePath = "user://key_settings.cfg";
//
	//public override void _Ready()
	//{
		//_keyMappings = new Dictionary<string, string>
		//{
			//{ "MoveUp", "ui_up" },
			//{ "MoveDown", "ui_down" },
			//{ "MoveLeft", "ui_left" },
			//{ "MoveRight", "ui_right" }
		//};
//
////		LoadSettings();
//
		////GetNode<Button>("VBoxContainer/HBoxContainer/WASDButton").Connect("pressed", this, nameof(OnWASDButtonPressed));
		////GetNode<Button>("VBoxContainer/HBoxContainer/ArrowKeysButton").Connect("pressed", this, nameof(OnArrowKeysButtonPressed));
		////GetNode<Button>("VBoxContainer/SaveButton").Connect("pressed", this, nameof(OnSaveButtonPressed));
	//}
//
	//private void OnWASDButtonPressed()
	//{
		//SetKeyBindings("W", "S", "A", "D");
	//}
//
	//private void OnArrowKeysButtonPressed()
	//{
		//SetKeyBindings("Up", "Down", "Left", "Right");
	//}
//
	//private void SetKeyBindings(string up, string down, string left, string right)
	//{
		//var e = new InputEventKey();
		//e.Set("scancode", KeyList.A);
		//InputMap.ActionAddEvent("place_bomb_p1", e);
		////InputMap.ActionEraseEvents("ui_up");
		////InputMap.ActionAddEvent("ui_up", new InputEventKey { PhysicalScancode = (uint)KeyList.W });
////
		////InputMap.ActionEraseEvents("ui_down");
		////InputMap.ActionAddEvent("ui_down", new InputEventKey { PhysicalScancode = (uint)KeyList.S });
////
		////InputMap.ActionEraseEvents("ui_left");
		////InputMap.ActionAddEvent("ui_left", new InputEventKey { PhysicalScancode = (uint)KeyList.A });
////
		////InputMap.ActionEraseEvents("ui_right");
		////InputMap.ActionAddEvent("ui_right", new InputEventKey { PhysicalScancode = (uint)KeyList.D });
//
		////GD.Print($"Set key bindings to: Up={up}, Down={down}, Left={left}, Right={right}");
	//}
//
	//private void OnSaveButtonPressed()
	//{
	////	SaveSettings();
	//}
//
	////private void SaveSettings()
	////{
		////var configFile = new ConfigFile();
////
		////foreach (var key in _keyMappings.Keys)
		////{
			////var action = _keyMappings[key];
			////var events = InputMap.GetActionList(action);
			////if (events.Count > 0 && events[0] is InputEventKey eventKey)
			////{
				////configFile.SetValue("Controls", key, eventKey.PhysicalScancode);
			////}
		////}
////
		////configFile.Save(SaveFilePath);
	////}
//
	////private void LoadSettings()
	////{
		////var configFile = new ConfigFile();
		////var err = configFile.Load(SaveFilePath);
////
		////if (err == Error.Ok)
		////{
			////foreach (var key in _keyMappings.Keys)
			////{
				////var scancode = (uint)configFile.GetValue("Controls", key, (uint)0);
				////if (scancode != 0)
				////{
					////var action = _keyMappings[key];
					////InputMap.ActionEraseEvents(action);
					////InputMap.ActionAddEvent(action, new InputEventKey { PhysicalScancode = scancode });
				////}
			////}
		////}
	////}
//}
