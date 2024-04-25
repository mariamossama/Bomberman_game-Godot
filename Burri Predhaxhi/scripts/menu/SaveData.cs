using Godot;
using System;
using System.IO;
using Dictionary =  Godot.Collections.Dictionary;
using Array = Godot.Collections.Array;
using System.Collections.Generic;
public partial class SaveData : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("ready");
		Dictionary data  = new Dictionary();
		data.Add("name","Tim");
		data.Add("name1","Anna");
		data.Add("level",12);
		data.Add("inventory",new Array{
			new Dictionary{
				{"Item Name","Sword"},
				{"Oty",1}
			},
			new Dictionary{
				{"Item Name","Coin"},
				{"Oty",11}
			},
		});
		GD.Print("ready1");
		string json = Json.Stringify(data);
		string path = ProjectSettings.GlobalizePath("user://");
		
		SaveTextToFile(path,"SaveGame1.json",json);

		string loadedData = LoadTextFromFile (path,"SaveGame1.json");
		GD.Print(LoadTextFromFile(path,"SaveGame1.json"));

		Json jsonLoader = new Json();
		Error error = jsonLoader.Parse(loadedData);
		if(error != Error.Ok){
			GD.Print(error);
			return;
		}
		Dictionary loadedDataDict = (Dictionary)jsonLoader.Data;
		GD.Print(loadedDataDict["inventory"].AsGodotArray()[0]);
	}
	private string LoadTextFromFile(string path,string fileName){
		string data = null;
		path = Path.Join(path,fileName);
		if(!File.Exists(path)) return null;
		try{
			data = File.ReadAllText(path);
		}catch(System.Exception e){
				GD.Print(e);
		}
		return data;
	}
	private void SaveTextToFile(string path,string fileName,string data){
		if(!Directory.Exists(path)){
			Directory.CreateDirectory(path);
		}
		path = Path.Join(path,fileName);
		GD.Print(path);

		try{
			File.WriteAllText(path,data);
		}catch(System.Exception e){
				GD.Print(e);
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
