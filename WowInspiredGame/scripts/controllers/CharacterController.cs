using Godot;
using System;
using WowInspiredGame.scripts.helpers;

public partial class CharacterController : CharacterBody3D
{
    [Export]
    public const float Speed = 5.0f;
    [Export]
    public const float JumpVelocity = 6f;
    [Export]
    public float TurnSpeed = 0.1f;

    private Node3D _playerLook;
    private bool _isAutorunEnabled = false;

    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    public delegate void PositionChanged(Vector3 position);
    public event PositionChanged OnPositionChanged;

    public override void _Ready() {
        _playerLook = GetNode<Node3D>("../CameraController/PlayerLook/LookTarget");
    }

    // Get the gravity from the project settings to be synced with RigidBody nodes.

    public override void _PhysicsProcess(double delta) {
        Vector3 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
            velocity.Y -= gravity * (float)delta;

        // Handle Jump.
        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
            velocity.Y = JumpVelocity;


        // Get the input direction and handle the movement/deceleration.
        Vector2 inputDir = Input.GetVector("Left", "Right", "Forward", "Back");
        Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

        HandleAutorun(_playerLook.GlobalPosition,ref velocity);

        if (direction != Vector3.Zero) {
            HadleLooking();

            velocity.X = direction.X * Speed;
            velocity.Z = direction.Z * Speed;
            _isAutorunEnabled = false;
        }
        else if (!_isAutorunEnabled) {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
        }

        if (OnPositionChanged != null) { OnPositionChanged(GlobalPosition); }
        Velocity = velocity;
        
        MoveAndSlide();
        GD.Print(_isAutorunEnabled);
    }

    private void HadleLooking() {
        LookAt(new Vector3(_playerLook.GlobalPosition.X, GlobalPosition.Y, _playerLook.GlobalPosition.Z));
    }

    private void HandleAutorun(Vector3 targetPosition, ref Vector3 velocity) {

        if (Input.IsActionJustPressed("NumLock")) {
            _isAutorunEnabled=true;
            Vector3 direction = GlobalPosition.DirectionTo(targetPosition);
            velocity = direction * Speed;
            HadleLooking();
        }
    }

}
