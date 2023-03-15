using System;
using Raylib_cs;

public class Level2
{
    public List<Rectangle> structure = new();
    public List<Rectangle> teleport = new();
    public List<Rectangle> wall = new();
    public List <Rectangle> block = new();
    public List <Rectangle> roof = new();
    public List <Rectangle> killFloor = new();



    public Level2()
    {
        // Golvet
        structure.Add(new Rectangle(0, 700, 250, 100)); 
        structure.Add(new Rectangle(450, 700, 1050, 100)); 
        // Plattformer
        structure.Add(new Rectangle(200,300,100,10));
        structure.Add(new Rectangle(400,500,300,10));
        // Tak
        roof.Add(new Rectangle(400,340,300,10));
        // Väggar
        wall.Add(new Rectangle(200,310,100,390));
        wall.Add(new Rectangle(400,200,100,140));
        // Block
        block.Add(new Rectangle(400, 510, 300, 190));
        block.Add(new Rectangle(400, 0, 100, 200));
        // Killfloor (Golv som TPar spelaren till början av leveln)
        killFloor.Add(new Rectangle(250,700,200,100));
        // Teleport(duh)
        teleport.Add(new Rectangle (1300,100, 100,100));
    }

}
