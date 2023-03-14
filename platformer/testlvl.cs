using System;
using Raylib_cs;

public class testlvl
{
    public List<Rectangle> structure = new();
    public List<Rectangle> teleport = new();
    public List<Rectangle> wall = new();
    public List <Rectangle> block = new();
    public List <Rectangle> roof = new();
    public List <Rectangle> killFloor = new();

    public testlvl(){
        // Golvet
        structure.Add(new Rectangle(0, 700, 1500, 100)); 
        // Plattformer
        structure.Add(new Rectangle(600, 600, 100, 10));
        structure.Add(new Rectangle(500, 390, 100, 10));
        structure.Add(new Rectangle(300, 190, 100, 10));
        structure.Add(new Rectangle(100, 100, 100, 10));
        // Tak
        roof.Add(new Rectangle (100, 590, 100, 10));
        // Väggar
        wall.Add(new Rectangle (500, 400, 100, 300));
        wall.Add(new Rectangle (300, 200, 100, 300));
        // Block
        block.Add(new Rectangle(100, 110, 100, 490));
        // Golv som TPar spelaren till början av leveln
        killFloor.Add(new Rectangle(900, 700, 100, 100));
        // Teleport(duh)
        teleport.Add(new Rectangle (1300,600, 100,100));
    }
}

