using Godot;
using System;

public partial class Menu : Control
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

		QuitGame();
		PressPlay();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void QuitGame()
	{
		quitButton.Pressed += () => GetTree().Quit();
	}

	public void PressPlay()
	{
		playButton.Pressed += () => GetTree().ChangeSceneToFile("res://Scenes/Game.tscn");
	}
}
