using Godot;
using System;

public partial class DebugPanel : ItemList
{
	public Label PositionLabel;
    public Label TargetLabel;
    public Label HealthLabel;


    private CharacterController _characterController;

	private float _speed;
	private float _name;

	public override void _Ready()
	{
        PositionLabel = GetNode("Position") as Label;
        TargetLabel = GetNode("Target") as Label;
        HealthLabel = GetNode("TargetHealth") as Label;


        var _characterController = GetTree().GetNodesInGroup("Player")[0] as CharacterController;
        //_characterController.OnPositionChanged += UpdatePositionLabel;
		UpdateTargetLabel("No target");
        UpdateTargetHealthLabel(0,0);
    }

    public override void _Process(double delta)
	{

	}

	private void UpdatePositionLabel(Vector3 speed) {
		PositionLabel.Text = String.Join(' ', "Position: " ,speed.ToString());
	}
	public void UpdateTargetLabel(string name) {
		TargetLabel.Text = String.Join(' ', "Target: ", name);
	}
    public void UpdateTargetHealthLabel(int actualHealth, int allHealth) {
        HealthLabel.Text = String.Join('/',actualHealth.ToString(),allHealth.ToString());
    }

}
