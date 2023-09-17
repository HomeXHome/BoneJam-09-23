using Godot;
using System;

public partial class HealthController : Node3D
{
	[Export]
	public int Health = 10;

	private int _actualHealth;

	public override void _Ready()
	{
		SetActualHealth(Health);
    }

	private void SetActualHealth(int health) {
		_actualHealth = health;
	}

	public void ChangeHealth(int incomingDamage) {
		_actualHealth -= incomingDamage;
		if (_actualHealth < 0) {
			_actualHealth = 0;
		}
	}

	public int[] ReturnAllHealth() {
		return new int[2] { _actualHealth, Health };
    }


}
