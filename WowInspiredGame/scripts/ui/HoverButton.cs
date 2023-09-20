using Godot;
using System;



public partial class HoverButton : TextureButton
{
    private Color _originalModulate;
    private Panel _tooltipPanel;
    private Label _tooltipLabelName;
    private Label _tooltipLabelDescription;

    public string TooltipLabelName;
    public string TooltipLabelDescription;

    public override void _Ready() {
        _originalModulate = Modulate;
        Connect("mouse_entered", Callable.From(_OnMouseEntered));
        Connect("mouse_exited", Callable.From(_OnMouseExited));
        _tooltipLabelName = GetParent()
            .GetParent()
            .GetParent()
            .GetParent()
            .GetNode<Panel>("TooltipPanel")
            .GetNode<VBoxContainer>("VBoxContainer")
            .GetNode<Label>("ItemName");

        _tooltipLabelDescription = GetParent()
            .GetParent()
            .GetParent()
            .GetParent()
            .GetNode<Panel>("TooltipPanel")
            .GetNode<VBoxContainer>("VBoxContainer")
            .GetNode<Label>("Description");

        TooltipLabelName = GetMeta("LabelName").ToString();
        TooltipLabelDescription = GetMeta("LabelDescription").ToString();

        _tooltipPanel = GetParent()
            .GetParent()
            .GetParent()
            .GetParent()
            .GetNode<Panel>("TooltipPanel");
    }

    private void _OnMouseEntered() {
        Modulate = new Color(1.2f, 1.2f, 1.2f, 1.0f);

        _tooltipLabelName.Text = TooltipLabelName;
        _tooltipLabelDescription.Text = TooltipLabelDescription;
        _tooltipPanel.Visible = true;     }

    private void _OnMouseExited() {
        Modulate = _originalModulate;
        _tooltipPanel.Visible = false;

    }


}
