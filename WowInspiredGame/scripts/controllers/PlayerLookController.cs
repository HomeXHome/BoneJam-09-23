using Godot;
using System;

public partial class PlayerLookController : Node3D
{
	private Node3D _cameraController;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        _cameraController = GetTree().GetNodesInGroup("CameraController")[0] as Node3D;

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
