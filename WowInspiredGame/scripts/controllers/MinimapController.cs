using Godot;
using System;

public partial class MinimapController : Camera3D
{
	public CharacterBody3D Player;
	public float CameraY = 50f;
	public override void _Ready()
	{
		Player = GetParent().
			GetParent().
			GetParent().
			GetParent().
			GetParent().
			GetNode<CharacterBody3D>("CharacterBody3D");
	}

	public override void _Process(double delta)
	{
		GlobalPosition = new Vector3(Player.GlobalPosition.X, CameraY, Player.GlobalPosition.Z);
	}
}
