using Godot;
using System;
using System.Collections.Generic;

public partial class TargetController : Node3D
{
    private List<Node3D> _closeCollisionShapes = new();
    private List<Node3D> _farCollisionShapes = new();

    [Signal]
    public delegate void ClickedTargetEventHandler(Vector3 clickLocation);
    [Signal]
    public delegate void ClickedTargetNoMovementEventHandler(Vector3 clickLocation);
    [Signal]
    public delegate void ClickedNameEventHandler(string objectName);
    [Signal]
    public delegate void HideFarTargetEventHandler();
    [Signal]
    public delegate void HidePickableTargetEventHandler();
    [Signal]
    public delegate void ClickedTargetHealthEventHandler(int health);
    [Signal]
    public delegate void ReturnTargetHealthEventHandler(int[] health);

    private Node3D _playerTarget = null;
    private AttackController _attackController;
    private HealthController _healthController;
    private int _currentAttackPower = 0;

    public override void _Ready() {
        _attackController = GetParent().GetNode<AttackController>("AttackController");

    }

    public void AddEntityToCloseList(Node3D collision) {
        if (collision.IsInGroup("Interactable"))
            _closeCollisionShapes.Add(collision);
    }

    public void RemoveEntityFromCloseList(Node3D collision) {
        if (collision.IsInGroup("Interactable"))
            _closeCollisionShapes.Remove(collision);
    }

    public void AddEntityToFarList(Node3D collision) {
        if (collision.IsInGroup("Interactable"))
            _farCollisionShapes.Add(collision);
    }

    /// <summary>
    /// Method will remove target ui and remove far away target from reference
    /// </summary>
    /// <param name="collision"></param>
    public void RemoveEntityFromFarList(Node3D collision) {
        if (collision.IsInGroup("Interactable")) {
            if (_playerTarget == collision) {
                ResetTargetForPlayer();
            }
            EmitSignal(nameof(HideFarTarget));
            _farCollisionShapes.Remove(collision);
        }
    }

    public bool CheckIfObjectIsFar(Node3D collision) {
        return _farCollisionShapes.Contains(collision);
    }
    public bool CheckIfObjectIsClose(Node3D collision) {
        return _closeCollisionShapes.Contains(collision);
    }

    public List<Node3D> ReturnAllNearObjects() {
        return _farCollisionShapes;
    }

    public void CheckIfTargeted(Node3D target, string objectName) {
        if (CheckIfObjectIsFar(target)) {
            EmitSignal(nameof(ClickedName), objectName);
            if (_playerTarget == target) {
                switch (CheckIfObjectIsClose(target)) {
                    case true:
                        EmitSignal(nameof(ClickedTargetNoMovement), target.GlobalPosition);
                        HandleAttack();
                        break;
                    case false:
                        EmitSignal(nameof(ClickedTarget), target.GlobalPosition);
                        break;
                }
            }
            else {
                SetTargetForPlayer(target);
            }
        }
    }

    private void SetTargetForPlayer(Node3D target) {
        _playerTarget = target;
        _healthController = GetNode<HealthController>($"{target.GetPath()}/HealthController");
    }
    private void ResetTargetForPlayer() {
        _playerTarget = null;
        _healthController = null;
    }

    public void HandleAttack() {
        if (_playerTarget.IsInGroup("Pickable")) {
            //TODO Pick
            PickUpAndRemoveObject();
            return;
        }
        if (_attackController.ReturnReady() && CheckIfObjectIsClose(_playerTarget)) {
            _attackController.HandleAttacking();
            _healthController.ChangeHealth(_currentAttackPower);
            EmitSignal(nameof(ClickedTargetHealth), _currentAttackPower);
            EmitSignal(nameof(ReturnTargetHealth), _healthController.ReturnAllHealth());
        }
    }

    public void HandleAutoAttack(bool isReadyToContinueAttack) {
        if (isReadyToContinueAttack && CheckIfObjectIsClose(_playerTarget)) {
            HandleAttack();
        }
    }

    public void SetAttackPower(int power) {
        _currentAttackPower = power;
    }

    public void PickUpAndRemoveObject() {
        _farCollisionShapes.Remove(_playerTarget);
        _playerTarget.Hide();
        ResetTargetForPlayer();
        EmitSignal(nameof(HidePickableTarget));
    }
}
