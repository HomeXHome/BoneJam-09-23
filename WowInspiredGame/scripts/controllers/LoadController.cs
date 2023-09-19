using Godot;
using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

public partial class LoadController : Node3D
{
    string appDomain = AppDomain.CurrentDomain.BaseDirectory;
    string _saveFolder = "save";
    string saveFileName = "saveGameFile.json";
    string combinePath;

    [Signal]
    public delegate void LoadGamePositionEventHandler(Vector3 position);

    [Signal]
    public delegate void LoadGameInventoryEventHandler(SaveModel saveModel);

    public override void _Ready()
	{
        combinePath = Path.Combine(appDomain, _saveFolder, saveFileName);
        //LoadGame(combinePath);
	}

	public void LoadGame() {
        if (CheckIfSaveExists(combinePath)) {
            DeserializeSaveFile(combinePath);
        };
	}

    public bool CheckIfSaveExists(string path) {
        if (!Directory.Exists(Path.GetDirectoryName(path))) {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }
        return File.Exists(path);
    }

    public void DeserializeSaveFile(string path) {
        string jsonContent = File.ReadAllText(path);
        SaveModel save = JsonConvert.DeserializeObject<SaveModel>(jsonContent);
        //EmitSignal(nameof(LoadGamePosition),save.PlayerPosition);
        //EmitSignal(nameof(LoadGameInventory), save);
    }


}
