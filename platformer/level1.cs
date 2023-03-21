using System;
using Raylib_cs;

public class Level1
{
    public List<Rectangle> structure = new();
    public List<Rectangle> teleport = new();
    public List<Rectangle> wall = new();
    public List <Rectangle> block = new();
    public List <Rectangle> roof = new();
    public List <Rectangle> killFloor = new();
    
    public Level1()
    {
        // Golvet
        structure.Add(new Rectangle(0, 700, 1500, 100)); 
        // Plattformer
        structure.Add(new Rectangle(200, 600, 200, 10));
        structure.Add(new Rectangle(400, 500, 200, 10));
        structure.Add(new Rectangle(600, 400, 200, 10));
        structure.Add(new Rectangle(800, 300, 450, 10));

        // Teleport(duh)
        teleport.Add(new Rectangle (1300,100, 100,100));
    }
    

}
