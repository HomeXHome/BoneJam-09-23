using Godot;
using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

public partial class SaveController : Node
{
    string appDomain = AppDomain.CurrentDomain.BaseDirectory;
    string _saveFolder = "save";
    string saveFileName = "saveGameFile.json";
    string saveFilePosName = "saveFilePosFile.json";


    private Vector3 _playerPos;
    private InventoryController _inventoryController;
    private Node3D _player;

    public void SaveGame() {
        CheckIfSaveExists();
        _player = GetParent().GetNode<Node3D>("Player").GetNode<CharacterBody3D>("CharacterBody3D");
        _inventoryController = GetParent()
            .GetNode<Node3D>("Player")
            .GetNode<CharacterBody3D>("CharacterBody3D")
            .GetNode<InventoryController>("InventoryController");
        var _tags = _inventoryController.ReturnInventoryList();
        using StreamWriter sw = new(Path.Combine(appDomain, _saveFolder, saveFileName));
        using StreamWriter sw1 = new(Path.Combine(appDomain, _saveFolder, saveFilePosName));

        JsonSerializer serializer = new JsonSerializer();
        serializer.Serialize(sw, _tags);
        serializer.Serialize(sw1, _player.GlobalPosition);
        sw.Close();
        sw1.Close();
        
    }

    public void CheckIfSaveExists() {
        if (!Directory.Exists(Path.Combine(appDomain, _saveFolder))) {
            Directory.CreateDirectory(Path.Combine(appDomain, _saveFolder));
        }
    }
}
