using Godot;
using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

public partial class LoadController : Node
{
    string appDomain = AppDomain.CurrentDomain.BaseDirectory;
    string _saveFolder = "save";
    string saveFileName = "saveGameFile.json";
    string combinePath;


    public override void _Ready() {
        combinePath = Path.Combine(appDomain, _saveFolder, saveFileName);

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
        var strings = jsonContent.Split('\n');
        foreach (var item in strings)
        {
            GD.Print(item);
        }
        //EmitSignal(nameof(LoadGamePosition),save.PlayerPosition);
        //EmitSignal(nameof(LoadGameInventory), save);
    }


}
