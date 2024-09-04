using Godot;
using System;
using System.Collections.Generic;

public partial class PrebuildGrid : Control
{
	[Signal]
	public delegate void GridIsCompletedEventHandler();
	List<Node> pieces = new List<Node>();
	Button backButton;
	Button optionsButton;
	Button startButton;
	Control scene;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GettingNodes();
		GetAnimalsInGame();
		PressBack();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void GettingNodes()
	{
		scene = GetNode<Control>(".");
		backButton = GetNode<Button>("HBoxContainer/Back/MarginContainer/Button");
		optionsButton = GetNode<Button>("HBoxContainer/Options/MarginContainer/Button");
		startButton = GetNode<Button>("HBoxContainer/Start/MarginContainer/Button");
	} 

	private void PressBack()
	{
		backButton.Pressed += () => GetTree().ChangeSceneToFile("res://Scenes/Menu.tscn");
	}
	private void PressStart()
	{
		bool canStart = false;
		if (canStart == CheckIfPiecesArePlaced())
		{
			GD.Print("Hellooooo");
			startButton.Pressed += () => GetTree().ChangeSceneToFile("res://Scenes/Menu.tscn");
		}
	}

	private void GetAnimalsInGame()
	{
		foreach (Control node in scene.GetChildren())
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
	private bool CheckIfPiecesArePlaced()
	{
		bool allPiecesHidden = true;

		foreach (Control node in pieces)
		{
			if (node.Visible == true)
			{
				allPiecesHidden = false;
				break;
			}
		}
		return allPiecesHidden;
	}
}
