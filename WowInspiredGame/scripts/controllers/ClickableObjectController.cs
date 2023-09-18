using Godot;
using System;

public partial class ClickableObjectController : StaticBody3D
{
    [Export]
    public string ObjectName;

    private CollisionShape3D _collisionObject;
    private HealthController healthController;
    private InventoryTag _tag;
    private TargetController _playerTargetController;
    private TargetInfoUI _targetInfoUI;

    //[Signal]
    //public delegate void ClickedObjectEventHandler(Node3D node, string Name);
    //[Signal]
    //public delegate void ClickedObjectHealthEventHandler(int[] health);



    public override void _Ready() {
        _collisionObject = GetNode<CollisionShape3D>("CollisionShape3D");
        healthController = GetNode<HealthController>("HealthController");
        _playerTargetController = GetParent()
            .GetNode<Node3D>("Player")
            .GetNode<CharacterBody3D>("CharacterBody3D")
            .GetNode<TargetController>("TargetController"); // I am doing THIS cause i can't figure out signals between scenes. For now it crashes with 'Condition "!common_parent" is true. Continuing.'
        _targetInfoUI = GetParent()
            .GetNode<Node3D>("Player")
            .GetNode<Node3D>("CameraController")
            .GetNode<CanvasLayer>("UI")
            .GetNode<TargetInfoUI>("TargetUIPanel");
    }

    public override void _Process(double delta) {

    }


    public override void _InputEvent(Camera3D camera,
                                     InputEvent @event,
                                     Vector3 position,
                                     Vector3 normal,
                                     int shapeIdx) {
        if (@event is InputEventMouseButton eventMouseButton
            && eventMouseButton.IsPressed()
            && eventMouseButton.ButtonIndex == MouseButton.Left) {
            _playerTargetController.CheckIfTargeted((Node3D)this, ObjectName);
            //EmitSignal(nameof(ClickedObject), (Node3D)this, ObjectName);
            GetHealthForUI();
        }
    }

    public void GetHealthForUI() {
        int[] healthValues = healthController.ReturnAllHealth();
        _targetInfoUI.ShowTargetHealthInUI(healthValues);
        //EmitSignal(nameof(ClickedObjectHealth), healthValues);
    }
}
