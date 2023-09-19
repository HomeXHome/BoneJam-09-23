using Godot;
using System;
using System.Collections.Generic;

public partial class InventoryUIPanel : PanelContainer
{
	private InventoryController _inventoryController;
	private GridContainer _gridContainer;
    public override void _Ready()
	{
        // IDK for now how to do it better, it's for TODO list
        GetInventoryList();

        _gridContainer = GetNode<GridContainer>("GridContainer");
    }

	public override void _Process(double delta)
	{
	}

	public void UpdateInventoryList(TextureButton button) {
        GetInventoryList();
        foreach (var item in _inventoryController.InventoryList) {
			_gridContainer.AddChild(button);
        }
	}
    public void UpdateInventoryList() {
        GetInventoryList();
        foreach (var item in _inventoryController.InventoryList) {
            CreateInventoryButton(item);
        }
    }

    public void CreateInventoryButton(Tag tag) {
        var button = new TextureButton();
		button.TextureNormal = tag.Sprite.Texture;
		button.SetMeta("LabelName", tag.ItemName);
        button.SetMeta("LabelDescription", tag.Description);
        _gridContainer ??= GetNode<GridContainer>("GridContainer"); // Problem with emitting signal on load and _ready of this object
        _gridContainer.AddChild(button);
    }

	public void GetInventoryList() {
        _inventoryController = GetParent().GetParent()
                                  .GetParent()
                                  .GetChild(0)
                                  .GetNode<InventoryController>("InventoryController");
    }
}
