using Godot;
using System;

public partial class AnimationController : Node3D
{
	private AnimationTree _animationTree;
    private float _blendSpeed = 0.1f; 
    private Vector2 _currentBlendPosition;

    public override void _Ready()
	{
		_animationTree = GetNode<AnimationTree>("AnimationTree");
	}

	public override void _Process(double delta)
	{
        UpdateWalkAnimation();
    }

	public void UpdateWalkAnimation() {
        Vector2 inputs = Input.GetVector("Left", "Right", "Forward", "Back");
        //_currentBlendPosition = _currentBlendPosition.Lerp(inputs, _blendSpeed);
        _animationTree.Set("parameters/blend/blend_position", inputs);

    }
}
