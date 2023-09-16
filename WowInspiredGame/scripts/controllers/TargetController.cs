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
    public delegate void ClickedNameEventHandler(string objectName);
    [Signal]
    public delegate void HideFarTargetEventHandler();

    private Node3D _playerTarget = null;

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
				_playerTarget = null;
				GD.Print("Убираю UI");  // TODO <--------------------------------------
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
			if (_playerTarget == target) {
				switch (CheckIfObjectIsClose(target)) {
					case true:
						GD.Print("Attacking!"); // TODO <--------------------------------------
                        break;
					case false:
                        EmitSignal(nameof(ClickedTarget), target.GlobalPosition);
                        EmitSignal(nameof(ClickedName), objectName);
					break;
                }
            } else {
				_playerTarget = target;
                EmitSignal(nameof(ClickedName), objectName);
                GD.Print("Вызываю таргет ui"); // TODO <--------------------------------------
            }
		}
	}
	}
