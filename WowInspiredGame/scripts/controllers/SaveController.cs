using Godot;
using System;
using Newtonsoft.Json;
using System.IO;

public partial class SaveController : Node3D
{
	string appDomain = AppDomain.CurrentDomain.BaseDirectory;
	string _saveFolder = "save";
	string saveFileName = "saveGameFile.json";

	private Vector3 _playerPos;
	private InventoryController _inventoryController;

    public void SaveGame() {
		CheckIfSaveExists();

        SaveModel save = new();
		_playerPos = GetParent().GetParent<Node3D>().GlobalPosition;
		_inventoryController = GetParent().GetNode<InventoryController>("InventoryController"); 

		save.PlayerPosition = _playerPos;
		save.InventoryList = _inventoryController.InventoryList;

        using StreamWriter sw = new(Path.Combine(appDomain, _saveFolder, saveFileName));
		JsonSerializer serializer = new();
		serializer.Serialize(sw, save);
    }

	public void CheckIfSaveExists() {
		if (!Directory.Exists(Path.Combine(appDomain,_saveFolder))) {
			Directory.CreateDirectory(Path.Combine(appDomain, _saveFolder));
		}
	}
}
