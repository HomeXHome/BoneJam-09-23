using Godot;
using System;

public partial class Enemy : Node
{
	[Export]
	public int Health = 10;

	private int _actualHealth;

	[Signal]
	public delegate void HeathChangeEventHandler(int actualHealth, int overalllHealth);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetActualHealth(Health);
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
		EmitSignal(nameof(HeathChange), _actualHealth, Health);
	}

	
}
