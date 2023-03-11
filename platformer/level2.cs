using System;
using Raylib_cs;

public class Level2
{
    public List<Rectangle> structure = new();
    public List<Rectangle> teleport = new();
    public List<Rectangle> wall = new();


    public Level2()
    {
        // Golvet
        structure.Add(new Rectangle(0, 700, 1500, 100)); 
        // Plattformer
        structure.Add(new Rectangle(600, 600, 100, 10));
        structure.Add(new Rectangle(500, 390, 100, 10));
        structure.Add(new Rectangle(300, 190, 100, 10));

        // Väggar
        wall.Add(new Rectangle (500, 400, 100, 300));
        wall.Add(new Rectangle (300, 200, 100, 300));
        // Teleport(duh)
        teleport.Add(new Rectangle (1300,100, 100,100));
    }

}
