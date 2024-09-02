using Godot;
using System;
using System.Collections.Generic;

public partial class Game : Control
{
	Button optionsButton;
	Button backButton;
	Control gameScene;
	List<Control> pieces;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gameScene = GetNode<Control>(".");
		backButton = GetNode<Button>("HBoxContainer/Back/MarginContainer/Button");
		optionsButton = GetNode<Button>("HBoxContainer/Options/MarginContainer/Button");
		DataIsDragSignal();
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

	public void DataIsDragSignal()
	{
		foreach (Control node in gameScene.GetChildren())
		{
			if (node.IsInGroup("Animals"))
			{
				pieces.Add(node);
				GD.Print(pieces);
			}
		}
	}
}
