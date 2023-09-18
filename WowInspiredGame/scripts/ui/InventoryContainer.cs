using Godot;
using System;
using System.Collections.Generic;

public partial class InventoryContainer : GridContainer
{
	
	public override void _Ready()
	{
        Connect("child_entered_tree", Callable.From<Node>(UpdateChildren));

    }

	public void UpdateChildren(Node node) {
		node.SetScript(ResourceLoader.Load("res://scripts/ui/HoverButton.cs"));
	}
}
