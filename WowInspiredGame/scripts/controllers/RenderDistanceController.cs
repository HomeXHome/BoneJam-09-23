using Godot;
using System;

public partial class RenderDistanceController : Area3D
{


    public override void _Ready() {
        Connect("body_entered",Callable.From<Node3D>(OnRenderDistanceEntered));
        Connect("body_exited", Callable.From<Node3D>(OnRenderDistanceLeft));
    }

    public override void _Process(double delta) {

    }


    public void OnRenderDistanceEntered(Node3D node) {
        if (!node.IsVisibleInTree() && node.IsInGroup("Renderable")) {
            node.GetParent().GetParent<Node3D>().Visible = true;
        }
    }

    public void OnRenderDistanceLeft(Node3D node) {
        if (node.IsVisibleInTree() && node.IsInGroup("Renderable")) {
            node.GetParent().GetParent<Node3D>().Visible = false;
        }
    }

    public void EnableAllVisibleNodes() {
        var node = (Area3D)this;
        foreach (Node3D targetNode in node.GetOverlappingBodies()) {
            if (targetNode.IsInGroup("Renderable")) {
                GD.Print(targetNode.GetParent().GetParent<Node3D>());
                targetNode.GetParent().GetParent<Node3D>().Visible = true;
            }
        }
    }
}
