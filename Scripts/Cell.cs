using Godot;
using System;

public partial class Grid : ColorRect
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
	GD.Print("Hello");
    var dropableCells = GetTree().GetNodesInGroup("Dropable");

		foreach (Node node in dropableCells)
		{
			if (node.IsInGroup("Dropable"))
			{

				return true;
			}
		}
        return false;
	}
    public override void _DropData(Vector2 atPosition, Variant data)
	{
		return ;
	}
}
