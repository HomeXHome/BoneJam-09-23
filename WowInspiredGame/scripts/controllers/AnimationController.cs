using Godot;
using System;

public partial class AnimationController : Node3D
{
	private AnimationTree _animationTree;
    private float _blendSpeed = 0.1f; 
    private Vector2 _currentBlendPosition;

    private Timer _jumpTimer;
    private Timer _attackTimer;
    private bool _isAutoRunEnabled = false;

    public override void _Ready()
	{
		_animationTree = GetNode<AnimationTree>("AnimationTree");
        _jumpTimer = GetNode<Timer>("JumpTimer");
        _attackTimer = GetNode<Timer>("AttackTimer");

    }

	public override void _Process(double delta)
	{
        HandleAutorunBool();
        UpdateWalkAnimation(); 
        UpdateJumpAnimation();
    }

    public void UpdateWalkAnimation() {
        Vector2 inputs = Input.GetVector("Left", "Right", "Forward", "Back");
        if (!_isAutoRunEnabled) {
            _animationTree.Set("parameters/blend/blend_position", inputs);
        }
        //_currentBlendPosition = _currentBlendPosition.Lerp(inputs, _blendSpeed);
    }
    public void UpdateWalkAnimation(Vector2 inputs) {
        _isAutoRunEnabled = true;
        _animationTree.Set("parameters/blend/blend_position", inputs);
    }

    public void HandleAutorunBool() {
        if (Input.GetVector("Left", "Right", "Forward", "Back") != new Vector2(0, 0)) {
            _isAutoRunEnabled = false;
        }
    }

    public void UpdateJumpAnimation() {
        if (Input.IsActionJustPressed("Jump")) {
            GD.Print(_animationTree.Get("parameters/States/current_state"));
            _animationTree.Set("parameters/States/transition_request", "jump");
            //_animationTree.Set("parameters/States/transition_request", "idle");
            _jumpTimer.Start();
        }
    }

    public void SetIdleState() {
        _animationTree.Set("parameters/States/transition_request", "idle");
    }

    public void HandleAttackAnimation() {
        _animationTree.Set("parameters/States/transition_request", "attack");
        _attackTimer.Start();
    }

}
