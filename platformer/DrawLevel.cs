using System;
using Raylib_cs;

public class DrawLevelClass
{
    // Ritar ut alla objekt förutom spelaren
    public void DrawLevel(List<Rectangle> structure, List<Rectangle> teleport, List<Rectangle> wall, List<Rectangle> block, List<Rectangle> roof, List<Rectangle> killFloor)
    {
        // rita bakgrund
        Raylib.ClearBackground(Color.BLACK);
        // ritar ut alla objekt av olika typ resten av metoden
        for (var i = 0; i < structure.Count; i++)
        {
            Raylib.DrawRectangleRec(structure[i], Color.RED);
        }
        for (var i = 0; i < teleport.Count; i++)
        {
            Raylib.DrawRectangleRec(teleport[i], Color.GREEN);
        }
        for (int i = 0; i < wall.Count; i++)
        {
        Raylib.DrawRectangleRec(wall[i], Color.PURPLE);
        }
        for (var i = 0; i < block.Count; i++)
        {
            Raylib.DrawRectangleRec(block[i], Color.BLUE);
        }
        for (var i = 0; i < roof.Count; i++)
        {
            Raylib.DrawRectangleRec(roof[i], Color.GRAY);
        }
        for (var i = 0; i < killFloor.Count; i++)
        {
            Raylib.DrawRectangleRec(killFloor[i], Color.ORANGE);
        }
    }
}
