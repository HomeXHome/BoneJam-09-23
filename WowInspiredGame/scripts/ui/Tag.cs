using Godot;
using System;

public partial class Tag : Node3D
{
    [Export] public string ItemName;
    [Export] public string Description;
    [Export] public Sprite2D Sprite;

    public override void _Ready() {
        Sprite = GetNode<Sprite2D>("Sprite2D");
        this.AddToGroup("Persist");
    }

}
