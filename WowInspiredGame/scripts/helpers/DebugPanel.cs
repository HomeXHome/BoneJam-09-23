using Godot;
using System;

public partial class DebugPanel : Panel
{
	public Label PositionLabel;

	private CharacterController _characterController;

	private float _speed;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        PositionLabel = GetNode("Position") as Label;
		var _characterController = GetTree().GetNodesInGroup("Player")[0] as CharacterController;
        //_characterController = GetNodeOrNull<CharacterController>("../Player/CharacterBody3D/CharacterController");
        _characterController.OnPositionChanged += UpdatePositionLabel;
    }
    
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{

	}

	private void UpdatePositionLabel(Vector3 speed) {
		PositionLabel.Text = String.Join(' ', "Position" ,speed.ToString());
	}
}
