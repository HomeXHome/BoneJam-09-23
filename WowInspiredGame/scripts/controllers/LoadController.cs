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
    string saveFilePosName = "saveFilePosFile.json";


    [Signal]
    public delegate void LoadGameInventoryEventHandler();
    [Signal]
    public delegate void LoadItemEventHandler(string name, string desc, string path);
    [Signal]
    public delegate void LoadGamePositionEventHandler(Vector3 position);

    public override void _Ready() {
        combinePath = Path.Combine(appDomain, _saveFolder, saveFileName);

        _inventoryController = GetParent()
            .GetNode<Node3D>("Player")
            .GetNode<CharacterBody3D>("CharacterBody3D")
            .GetNode<InventoryController>("InventoryController");
        LoadGame();
    }

    public void LoadGame() {
        if (CheckIfSaveExists(combinePath)) {
            DeserializeSaveFile();
        }else {
            Node3D startNode = (Node3D)GetTree().GetFirstNodeInGroup("SpawnPoint");
            Vector3 startPos = startNode.GlobalPosition;
            EmitSignal(nameof(LoadGamePosition), startPos);
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
        var jsonContent1 = File.ReadAllText(Path.Combine(appDomain, _saveFolder, saveFilePosName));

        var result = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jsonContent);
        var resultPos = JsonConvert.DeserializeObject<Vector3>(jsonContent1);

        EmitSignal(nameof(LoadGameInventory));
        foreach (var kvp in result) {
            List<string> values = kvp.Value;
            EmitSignal(nameof(LoadItem), values[0], values[1], values[2]);
        }
        _inventoryController.SetInventoryList(result);
        EmitSignal(nameof(LoadGamePosition), resultPos);
    }


}
