using Godot;
using System;

public partial class GridMap : Control
{
    const int CELL_SIZE = 64;
    Control gridMap;

    public override void _Ready()
    {
        gridMap = GetNode<Control>(".");
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        // Calculer la position sur la grille
        Vector2 gridPosition = (atPosition / CELL_SIZE).Floor() * CELL_SIZE;

        // Vérifier si une pièce existe déjà à cette position
        foreach (Node child in gridMap.GetChildren())
        {
            if (child is TextureRect textureRect)
            {
                if (textureRect.Position == gridPosition)
                {
                    return false;
                }
            }
        }
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
    }
}
