using Godot;
using System;
using System.Collections.Generic;

public partial class PrebuildGrid : Control
{
	[Signal]
	public delegate void GridIsCompletedEventHandler();
	List<Node> pieces = new List<Node>();
	Button optionsButton;
	Button backButton;
	Button startButton;
	Control gameScene;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gameScene = GetNode<Control>(".");
		backButton = GetNode<Button>("HBoxContainer/Back/MarginContainer/Button");
		optionsButton = GetNode<Button>("HBoxContainer/Options/MarginContainer/Button");
		startButton = GetNode<Button>("HBoxContainer/Start/MarginContainer/Button");
		GetAnimalsInGame();
		PressBack();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void PressBack()
	{
		backButton.Pressed += () => GetTree().ChangeSceneToFile("res://Scenes/Menu.tscn");
	}
	private void PressStart()
	{
		startButton.Pressed += () => GetTree().ChangeSceneToFile("res://Scenes/Menu.tscn");
	}

	private void GetAnimalsInGame()
	{
		foreach (Control node in gameScene.GetChildren())
		{
			if (node.IsInGroup("Animals"))
			{
				pieces.Add(node);
				Piece piece = node as Piece;
				if (piece != null)
				{
					piece.Connect(nameof(Piece.DataIsDrag), new Callable(this, nameof(OnDataIsDrag)));
				}
			}
		}
	}
	private void OnDataIsDrag(string animalsName)
	{
		foreach (Control node in pieces)
		{		
			if (node.Name == animalsName)
			{
				node.Visible = false;
			}
		}	
	}
}
