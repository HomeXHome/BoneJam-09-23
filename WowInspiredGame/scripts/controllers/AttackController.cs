using Godot;
using System;

public partial class AttackController : Node3D
{
    [Export]
    public double AttackCooldown = 2.5;
    [Export]
    public int AttackPower = 10;

    private bool _isReadyToAttack = true;
    private Timer _timer;

    [Signal]
    public delegate void AttackEventHandler(int attackPower);
    [Signal]
    public delegate void CooldownEventHandler(bool isReady);

    public override void _Ready() {
        _timer = GetNode<Timer>("CooldownTimer");
        _timer.WaitTime = AttackCooldown;
        _timer.Start();
    }

    public override void _Process(double delta) {
        //GD.Print(_isReadyToAttack);
    }

    public void HandleAttacking() {
        if (_isReadyToAttack) {
            _timer.Start();
            _isReadyToAttack = false;
            GD.Print("Attacking!"); 
            EmitSignal(nameof(Attack), AttackPower);
            EmitSignal(nameof(Cooldown), _isReadyToAttack);
        }
    }

    public void HandleReadyToAttackEmit() {
        _isReadyToAttack = true;
        EmitSignal(nameof(Cooldown), _isReadyToAttack);
        GD.Print("Reay To Attack Again");
    }

    public bool ReturnReady() {
        return _isReadyToAttack;
    }
}
