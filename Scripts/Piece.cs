using Godot;
using Godot.Collections;
using System;
using System.Runtime.CompilerServices;

public partial class Piece : Control
{
	[Export] private string _textureName;
	TextureRect texture;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		texture = GetNode<TextureRect>("TextureRect");
		Texture2D loadedTexture = GD.Load<Texture2D>($"res://Assets/{_textureName}.png");
		texture.Texture = loadedTexture;
	}
	public override Variant _GetDragData(Vector2 atPosition)
	{
		TextureRect preview = new TextureRect();
        preview.Texture = texture.Texture;
        preview.CustomMinimumSize = new Vector2(50, 50);
		SetDragPreview(preview);
		return texture;
	}
}
