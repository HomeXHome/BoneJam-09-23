using Godot;
using System;

public partial class ClickableObjectController : StaticBody3D
{
    [Export]
    public string ObjectName;

    private CollisionShape3D _collisionObject;
    private HealthController healthController;

    [Signal]
    public delegate void ClickedObjectEventHandler(Node3D node, string Name);
    [Signal]
    public delegate void ClickedObjectHealthEventHandler(int[] health);

    public override void _Ready() {
        _collisionObject = GetNode<CollisionShape3D>("CollisionShape3D");
        healthController = GetNode<HealthController>("HealthController");

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
            EmitSignal(nameof(ClickedObject), (Node3D)this, ObjectName);
            GetHealthForUI();
        }
    }

    public void GetHealthForUI() {
        int[] healthValues = healthController.ReturnAllHealth();
        EmitSignal(nameof(ClickedObjectHealth), healthValues);
    }
}
