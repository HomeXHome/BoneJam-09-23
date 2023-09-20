using Godot;
using System;
using System.Collections.Generic;

public partial class InventoryUIPanel : PanelContainer
{
	private GridContainer _gridContainer;
    public override void _Ready()
	{
        _gridContainer = GetNode<MarginContainer>("MarginContainer").GetNode<GridContainer>("GridContainer");
    }

	public override void _Process(double delta)
	{
	}


	public void CreateInventoryButton(string name, string desc, string path) {
        var button = new TextureButton();
		button.TextureNormal = (Texture2D)ResourceLoader.Load(path);
		button.SetMeta("LabelName", name);
        button.SetMeta("LabelDescription", desc);
        _gridContainer.AddChild(button);
    }


    public void RemoveAllButtons() {
        var btnArray = _gridContainer.GetChildren();
        foreach (var btn in btnArray) {
            btn.QueueFree();
        }
    }

}
