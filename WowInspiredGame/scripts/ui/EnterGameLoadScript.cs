using Godot;
using System;

public partial class EnterGameLoadScript : Button
{

    public override void _Ready() {
        Connect("pressed", Callable.From(ChangeSceneToGame));
    }
    public void ChangeSceneToGame() {
		GetTree().ChangeSceneToFile("res://scenes/levels/map_0.tscn");
	}
}
