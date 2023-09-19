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

    private Vector3 _playerPos;
    private InventoryController _inventoryController;
    private List<Tag> _tags;

    public void SaveGame() {
        CheckIfSaveExists();

        _inventoryController = GetParent()
            .GetNode<Node3D>("Player")
            .GetNode<CharacterBody3D>("CharacterBody3D")
            .GetNode<InventoryController>("InventoryController");
        _tags = _inventoryController.ReturnInventoryList();
        foreach (Tag tag in _tags) {
            using StreamWriter sw = new(Path.Combine(appDomain, _saveFolder, saveFileName));
            sw.WriteLine(tag);
        }
    }

    public void CheckIfSaveExists() {
        if (!Directory.Exists(Path.Combine(appDomain, _saveFolder))) {
            Directory.CreateDirectory(Path.Combine(appDomain, _saveFolder));
        }
    }
}
