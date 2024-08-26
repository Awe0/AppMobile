using Godot;
using System;

public partial class Game : Control
{
	Button optionsButton;
	Button backButton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		backButton = GetNode<Button>("HBoxContainer/Back/MarginContainer/Button");
		optionsButton = GetNode<Button>("HBoxContainer/Options/MarginContainer/Button");

		PressBack();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

		public void PressBack()
	{
		backButton.Pressed += () => GetTree().ChangeSceneToFile("res://Scenes/Menu.tscn");
	}
}
