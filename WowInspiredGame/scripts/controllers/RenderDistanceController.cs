using Godot;
using System;

public partial class RenderDistanceController : Area3D
{


    public override void _Ready() {
        Connect("body_entered",Callable.From<Node3D>(OnRenderDistanceEntered));
        Connect("body_exited", Callable.From<Node3D>(OnRenderDistanceLeft));

    }

    public void OnRenderDistanceEntered(Node3D node) {
        GD.Print($"{node} entered", node.IsVisibleInTree(), node.IsInGroup("Renderable"), node.GetGroups());
        if (!node.IsVisibleInTree() && node.IsInGroup("Renderable")) {
            node.Visible = true;
        }
    }

    public void OnRenderDistanceLeft(Node3D node) {
        if (node.IsVisibleInTree() && node.IsInGroup("Renderable")) {
            node.Visible = false;
        }
    }
}
