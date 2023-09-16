using Godot;
using System;

public partial class TargetInfoUI : PanelContainer
{
	private Label _nameLabel;
	private PanelContainer _container;

	public override void _Ready()
	{
		_container = GetNode<PanelContainer>(this.GetPath());
        _nameLabel = GetChild(0).GetNode<Label>("TargetName");

		HideTargetUI();
	}

	public override void _Process(double delta)
	{

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
}
