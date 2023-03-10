using System;
using Raylib_cs;

public class Level2
{
    public List<Rectangle> structure = new();
    public List<Rectangle> teleport = new();

    public Level2()
    {
        // Golvet
        structure.Add(new Rectangle(0, 700, 1500, 100)); 
        // Plattformer


        // Teleport(duh)
        teleport.Add(new Rectangle (1300,100, 100,100));
    }

}
