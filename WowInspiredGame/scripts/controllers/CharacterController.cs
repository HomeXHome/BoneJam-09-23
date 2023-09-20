using Godot;
using System;
using WowInspiredGame.scripts.helpers;

public partial class CharacterController : CharacterBody3D
{
    [Export]
    public float Speed = 8.0f;
    [Export]
    public const float JumpVelocity = 6f;
    [Export]
    public float TurnSpeed = 0.1f;

    private float stoppingDistance = 2.5f;
    private Node3D _playerLook;
    private bool _isAutorunEnabled = false;

    private bool _isTargetRunEnabled = false;
    private Vector3 _targetPosition = Vector3.Zero;

    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    //public delegate void PositionChanged(Vector3 position);
    //public event PositionChanged OnPositionChanged;

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

            HandleLooking();

            velocity.X = direction.X * Speed;
            velocity.Z = direction.Z * Speed;
            _isAutorunEnabled = false;
            _isTargetRunEnabled = false;
        }
        // Stopping if no input
        else if (!_isAutorunEnabled) {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
        }

        // Autorun here
        if (_targetPosition != Vector3.Zero && _isTargetRunEnabled) {
            Vector3 targetVector = (_targetPosition - GlobalPosition).Normalized();
            velocity.X = targetVector.X * Speed;
            velocity.Z = targetVector.Z * Speed;

            //Stopping before reaching target
            float distanceToTarget = (_targetPosition - GlobalPosition).Length();
            if (distanceToTarget < stoppingDistance) {
                velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
                velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
            }
        }

        //DEBUG ZONE--------------------------------------------------------------
        //OnPositionChanged?.Invoke(VectorRounder.RoundVector(GlobalPosition, 3));
        //------------------------------------------------------------------------

        Velocity = velocity;
        
        MoveAndSlide();
    }

    /// <summary>
    /// Handle looking towards movement
    /// </summary>
    private void HandleLooking() {
        LookAt(new Vector3(_playerLook.GlobalPosition.X, GlobalPosition.Y, _playerLook.GlobalPosition.Z));
    }

    /// <summary>
    /// Handle looking towards target position. 
    /// </summary>
    /// <param name="position"> The target position at which the player needs to be looking.</param>
    private void HandleLooking(Vector3 position) {
        if (position != GlobalPosition) {
            LookAt(new Vector3(position.X, GlobalPosition.Y, position.Z));
        }
    }

    /// <summary>
    /// Method for autorun.
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <param name="velocity"></param>
    private void HandleAutorun(Vector3 targetPosition, ref Vector3 velocity) {

        if (Input.IsActionJustPressed("NumLock")) {
            _isAutorunEnabled=true;
            Vector3 direction = GlobalPosition.DirectionTo(targetPosition);
            velocity = direction * Speed;
            HandleLooking();
        }
    }

    private void OnTargetClicked(Vector3 targetPosition) {
        _isTargetRunEnabled=true;
        HandleLooking(targetPosition);
        _targetPosition = targetPosition;
    }

    private void LoadPlayerPosition(Vector3 position) {
        GlobalPosition = position;
    }
}
