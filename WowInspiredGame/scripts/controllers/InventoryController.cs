using Godot;
using System;
using System.Collections.Generic;

public partial class InventoryController : Node3D
{
	public Dictionary<string,List<string>> InventoryDict = new();
	public List<string> InventoryName = new();

	[Signal]
	public delegate void NewItemAddedEventHandler(string name, string desc, string path);
	public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{

	}

	public void AddTag(string name, string desc, string path) {
		if (!InventoryDict.ContainsKey(name)) {
			List<string> list = new List<string>() {
				name,desc,path
			};
			InventoryDict.Add(name,list);
            EmitSignal(nameof(NewItemAdded), name,desc,path);
		}
	}

	public Dictionary<string,List<string>> ReturnInventoryList() {
		return InventoryDict;
	}

	public void SetInventoryList(Dictionary<string, List<string>> _inventoryList) {
        InventoryDict.Clear();
        InventoryDict = _inventoryList;
        //foreach (var kvp in InventoryDict) {
        //    string name = kvp.Key; 
        //    List<string> values = kvp.Value;

        //    AddTag(name, values[0], values[1]);
        //}
    }



	public void ResetInventory() {
        InventoryDict.Clear();
        InventoryName.Clear();
    }

}
