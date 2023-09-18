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

	public void CreateInventoryButton(Tag tag) {
        var buttonScene = GD.Load<PackedScene>("scenes/UI/texture_button.tscn");
        TextureButton button = (TextureButton)buttonScene.Instantiate();
		button.TextureNormal = tag.Sprite.Texture;
		button.SetMeta("LabelName", tag.ItemName);
        button.SetMeta("LabelDescription", tag.Description);
        _gridContainer.AddChild(button);
    }

	public void GetInventoryList() {
        _inventoryController = GetParent().GetParent()
                                  .GetParent()
                                  .GetChild(0)
                                  .GetNode<InventoryController>("InventoryController");
    }
}
