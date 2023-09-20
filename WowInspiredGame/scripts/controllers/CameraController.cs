using Godot;
using System;

public partial class CameraController : Node3D
{
    [Export]
    public int Sensitivity = 3;
    [Export]
    public float TopCameraBoundry = -0.8f;
    [Export]
    public float BottomCameraBoundry = 0.8f;

    private float _positionFromPlayerX = 0;
    private float _positionFromPlayerY = 2f;
    private float _positionFromPlayerZ = 0;

    private float _maxZoom = 16f;
    private float _minZoom = 2f;
    private float _zoomStep = 0.2f;
    private float _zoomSpeed = 0.8f;

    private bool _isMouseHidden = false;

    private SpringArm3D _springArm;
    private CharacterBody3D _character;
    private Camera3D _camera;
    private Node3D _lookAt;


    public override void _Ready() {

        _springArm = GetNode<SpringArm3D>("SpringArm3D");
        _character = GetTree().GetNodesInGroup("Player")[0] as CharacterBody3D;
        _camera = GetNode<Camera3D>("SpringArm3D/Camera3D");
        _lookAt = GetTree().GetNodesInGroup("LookAt")[0] as Node3D;
    }

    public override void _Process(double delta) {

        GlobalPosition = _character.Position + CameraStandardPos(); // Set camera controller pos following character
        _camera.LookAt(GetLookAt(_lookAt)); //Set Look At for Camera3D based on Node 3D with the name "LookAt"
        ActiveCameraCheck(); // Hides/Shows mouse


        //Debug Zone
        //GD.Print(_camera.Position);
        //GD.Print(_playerLook.GlobalPosition);
    }

    public override void _Input(InputEvent @event) {
        base._Input(@event);

        RotateCamera(@event);
        HanldeCameraZoom(@event);

    }


    private void RotateCamera(InputEvent @event) {
        if (@event is InputEventMouseMotion && _isMouseHidden) {
            //Camera rotating
            InputEventMouseMotion motion = (InputEventMouseMotion)@event;
            Rotation = new Vector3(Math.Clamp(Rotation.X - motion.Relative.Y / 1000 * Sensitivity,
                TopCameraBoundry, BottomCameraBoundry),
                Rotation.Y - motion.Relative.X / 1000 * Sensitivity, 0);
        }

    }

    //Method for Hiding mouse when Right Click is pressed
    private void ActiveCameraCheck() {
        if (Input.IsActionPressed("RightClick")) {
            //Hide Mouse
            _isMouseHidden = true;
            Input.MouseMode = Input.MouseModeEnum.Captured;
        }
        else {
            //Show mouse
            _isMouseHidden = false;
            Input.MouseMode = Input.MouseModeEnum.Visible;
        }
    }

    //Get "Standard" camera positon based on variables
    private Vector3 CameraStandardPos() {
        return new Vector3(_positionFromPlayerX, _positionFromPlayerY, _positionFromPlayerZ);
    }

    private Vector3 GetLookAt(Node3D character) {
        return character.GlobalPosition;
    }

    
    private void HanldeCameraZoom(InputEvent @event) {
        
        if (@event is InputEventMouseButton mouseEvent){
            switch (mouseEvent.ButtonIndex) {
                case MouseButton.WheelUp:
                    _springArm.SpringLength = Mathf.Clamp(_springArm.SpringLength + _zoomStep,_minZoom,_maxZoom);
                    break;

                case MouseButton.WheelDown:
                    _springArm.SpringLength = Mathf.Clamp(_springArm.SpringLength - _zoomStep, _minZoom, _maxZoom);
                    break;
            }
        }
    }


}
