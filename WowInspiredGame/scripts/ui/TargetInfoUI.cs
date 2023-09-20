using Godot;
using System;

public partial class TargetInfoUI : PanelContainer
{
	private Label _nameLabel;
	private Label _healthLabel;
	private PanelContainer _container;

	public override void _Ready()
	{
		_container = GetNode<PanelContainer>(this.GetPath());
        _nameLabel = GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("TargetName");
        _healthLabel = GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Health");

        HideTargetUI();
	}

    public void ShowTargetUI() {
        _container.Show();
	}

	public void HideTargetUI() {
        _container.Hide();
	}

	public void ShowTargetNameOnUI(string name) {
		ShowTargetUI();
        _nameLabel.Text = name;
	}

	public void ShowTargetHealthInUI(int[] health) {
		_healthLabel.Text = String.Join("/", health[0], health[1]);
	}
}
