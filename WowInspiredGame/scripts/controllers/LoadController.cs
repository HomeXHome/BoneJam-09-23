using Godot;
using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Text;

public partial class LoadController : Node
{
    string appDomain = AppDomain.CurrentDomain.BaseDirectory;
    string _saveFolder = "save";
    string saveFileName = "saveGameFile.json";
    string combinePath;
    private InventoryController _inventoryController;

    [Signal]
    public delegate void LoadGameInventoryEventHandler();
    [Signal]
    public delegate void LoadItemEventHandler(string name, string desc, string path);

    public override void _Ready() {
        combinePath = Path.Combine(appDomain, _saveFolder, saveFileName);

        _inventoryController = GetParent()
            .GetNode<Node3D>("Player")
            .GetNode<CharacterBody3D>("CharacterBody3D")
            .GetNode<InventoryController>("InventoryController");
    }

    public void LoadGame() {
        if (CheckIfSaveExists(combinePath)) {
            DeserializeSaveFile();
        };
    }

    public bool CheckIfSaveExists(string path) {
        if (!Directory.Exists(Path.GetDirectoryName(path))) {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }
        return File.Exists(path);
    }

    public void DeserializeSaveFile() {
        var jsonContent = File.ReadAllText(Path.Combine(appDomain, _saveFolder, saveFileName));
        var result = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jsonContent);
        foreach (var kvp in result) {
            List<string> values = kvp.Value;
            EmitSignal(nameof(LoadGameInventory));
            EmitSignal(nameof(LoadItem), values[0], values[1], values[2]);
            //AddTag(name, values[0], values[1]);
        }
        //_inventoryController.SetInventoryList(deltaList);
        //EmitSignal(nameof(LoadGamePosition),save.PlayerPosition);
    }


}
