using Godot;
using System;
using System.Collections.Generic;

public partial class InventoryController : Node3D
{
	public List<Tag> InventoryList = new();

	[Signal]
	public delegate void NewItemAddedEventHandler(Tag tag);
    [Signal]
    public delegate void LoadItemsEventHandler();
    public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{

	}

	public void AddTag(Tag tag) {
		if (!InventoryList.Contains(tag)) {
			InventoryList.Add(tag);
			EmitSignal(nameof(NewItemAdded), tag);
		}
	}

	public void LoadGameInventoryUpdate(SaveModel saveModel) {
		InventoryList = saveModel.InventoryList;
		foreach (Tag tag in InventoryList) {
            EmitSignal(nameof(LoadItems));
        }
    }
}
