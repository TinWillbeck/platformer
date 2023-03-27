using System;
using Raylib_cs;

public class Level2
{
    // Använder mig av listor för att jag vet ej hur många ojbekt jag vill ha i varje level, darför fungerar lisot bättre

    // Golv
    public List<Rectangle> structure = new();
    // Teleports
    public List<Rectangle> teleport = new();
    // Väggar man kan klättra på
    public List<Rectangle> wall = new();
    // Väggar man inte kan klättra på
    public List <Rectangle> block = new();
    // Tak
    public List <Rectangle> roof = new();
    // Golv man dör på
    public List <Rectangle> killFloor = new();



    public Level2()
    {
        // Golvet
        structure.Add(new Rectangle(0, 700, 1500, 100)); 
        structure.Add(new Rectangle(300, 600, 300, 100)); 

        // Plattformer
        structure.Add(new Rectangle(200,400,100,10));
        structure.Add(new Rectangle(600,400,100,10));
        structure.Add(new Rectangle(1200,200,100,10));
        // Tak
        roof.Add(new Rectangle(400, 490, 100, 10));
        // Väggar
        wall.Add(new Rectangle(200, 410, 100, 290));
        wall.Add(new Rectangle(600, 410, 100, 290));
        wall.Add(new Rectangle(1200, 210, 100, 140));
        // Block
        block.Add(new Rectangle(400, 0, 100, 490));
        // Killfloor (Golv som TPar spelaren till början av leveln)

        // Teleport
        teleport.Add(new Rectangle (1400,100, 100,100));
    }

}
