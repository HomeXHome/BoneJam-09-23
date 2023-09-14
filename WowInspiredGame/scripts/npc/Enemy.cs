using Godot;
using System;

public partial class Enemy : Node
{
	[Export]
	public int Health = 10;

	private int _actualHealth;
    private StaticBody3D _parentNode;

    [Signal]
	public delegate void HealthChangeEventHandler(int actualHealth, int overalllHealth);
	[Signal]
	public delegate void HealthEmitterEventHandler(int actualHealth, int overalllHealth);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetActualHealth(Health);
        _parentNode = GetParent() as StaticBody3D;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

    }

	private void SetActualHealth(int health) {
		_actualHealth = health;
	}

	public void ChangeHealth(int incomingDamage) {
		_actualHealth -= incomingDamage;
		EmitSignal(nameof(HealthChange), _actualHealth, Health);
	}

	public void EmitHealth(Vector3 vector) {
		EmitSignal(nameof(HealthEmitter), _actualHealth, Health);
    }


}
