using Godot;
using System;

public partial class Menu : Node
{
	Button playButton;
	Button optionsButton;
	Button quitButton;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playButton = GetNode<Button>("VBoxContainer/Play/MarginContainer/Button");
		optionsButton = GetNode<Button>("VBoxContainer/Options/MarginContainer/Button");
		quitButton = GetNode<Button>("VBoxContainer/Quit/MarginContainer/Button");

		MouseEnteredAndExited();
		QuitGame();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void MouseEnteredAndExited()
	{
		playButton.MouseEntered += () => playButton.Text = "Play";
		optionsButton.MouseEntered += () => optionsButton.Text = "Options";
		quitButton.MouseEntered += () => quitButton.Text = "Quit";

		playButton.MouseExited += () => playButton.Text = "";
		optionsButton.MouseExited += () => optionsButton.Text = "";
		quitButton.MouseExited += () => quitButton.Text = "";
	}

	public void QuitGame()
	{
		quitButton.Pressed += () => GetTree().Quit();
	}
}
