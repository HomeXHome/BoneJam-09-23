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
    private Vector3 _lastLookAtDirection = new Vector3();


    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    public override void _Ready() {
        _playerLook = GetTree().GetNodesInGroup("LookTarget")[0] as Node3D;
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
        // As good practice, you should replace UI actions with custom gameplay actions.
        Vector2 inputDir = Input.GetVector("Left", "Right", "Forward", "Back");
        Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
        if (direction != Vector3.Zero) {
            HandleLooking();
            velocity.X = direction.X * Speed;
            velocity.Z = direction.Z * Speed;
        }
        else {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
        }

        Velocity = velocity;
        MoveAndSlide();
    }

    private void HandleLooking() {
        Vector3 lookAt = _playerLook.GlobalPosition;
        lookAt = new Vector3(lookAt.X, GlobalPosition.Y, lookAt.Z);
        Vector3 lerpDirection = LerpHelper.LerpVector3(_lastLookAtDirection,lookAt,TurnSpeed);
        LookAt(lerpDirection);
        _lastLookAtDirection = lerpDirection;
    }

}
