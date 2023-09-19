using Godot;
using System;
using System.Collections.Generic;

public partial class InventoryUIPanel : PanelContainer
{
	private GridContainer _gridContainer;
    public override void _Ready()
	{
        _gridContainer = GetNode<GridContainer>("GridContainer");
    }

	public override void _Process(double delta)
	{
	}


	public void CreateInventoryButton(Tag tag) {
        var button = new TextureButton();
		button.TextureNormal = tag.Sprite.Texture;
		button.SetMeta("LabelName", tag.ItemName);
        button.SetMeta("LabelDescription", tag.Description);
        _gridContainer.AddChild(button);
    }

	public List<Tag> GetInventoryList() {
        return GetParent().GetParent()
                                  .GetParent()
                                  .GetChild(0)
                                  .GetNode<InventoryController>("InventoryController")
                                  .InventoryList;
    }

    public void RemoveAllButtons() {
        var btnArray = _gridContainer.GetChildren();
        foreach (var btn in btnArray) {
            btn.QueueFree();
        }
    }

    public void LoadAllButtons() {
        _gridContainer ??= GetNode<GridContainer>("GridContainer");
        RemoveAllButtons();
        List<Tag> buttons = GetInventoryList();
        foreach (Tag btn in buttons) {
            CreateInventoryButton(btn);
        }
    }
}
