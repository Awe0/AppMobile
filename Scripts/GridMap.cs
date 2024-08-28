using Godot;
using System;

public partial class GridMap : Control
{
	const int CELL_SIZE = 64;
	Control gridMap;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gridMap = GetNode<Control>(".");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
		return true;
    }
    public override void _DropData(Vector2 atPosition, Variant data)
    {
		if (data.Obj is TextureRect textureRect)
		{
			Vector2 gridPosition = (atPosition / CELL_SIZE).Floor() * CELL_SIZE;

			TextureRect newTextureRect = (TextureRect)textureRect.Duplicate();
			newTextureRect.Position = gridPosition;

			gridMap.AddChild(newTextureRect);
		}
		else
		{
			GD.Print("Le type de data n'est pas un TextureRect.");
		}
        base._DropData(atPosition, data);
    }
}
