using Godot;
using System;
using System.Collections.Generic;

public partial class InventoryController : Node3D
{
	public List<Tag> InventoryList = new();
	public List<string> InventoryName = new();

	[Signal]
	public delegate void NewItemAddedEventHandler(Tag tag);
	public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{

	}

	public void AddTag(Tag tag) {
		if (!InventoryName.Contains(tag.ItemName)) {
			InventoryName.Add(tag.ItemName);
			InventoryList.Add(tag);
			EmitSignal(nameof(NewItemAdded), tag);
		}
	}

	public List<Tag> ReturnInventoryList() {
		return InventoryList;
	}

	public void SetInventoryList(List<Tag> _inventoryList) {
		InventoryList.Clear();
		InventoryList = _inventoryList;
		foreach (Tag tag in InventoryList) {
			AddTag(tag);
        }

    }
}
