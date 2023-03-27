using System;
using Raylib_cs;

public class Level3
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

    public Level3(){
        // Golvet
        structure.Add(new Rectangle(0, 700, 200, 100)); 
        structure.Add(new Rectangle(1100, 700, 400, 100)); 
        // Plattformer
        structure.Add(new Rectangle(200,300,100,10));
        structure.Add(new Rectangle(400,500,300,10));
        structure.Add(new Rectangle(800,150,100,10));
        structure.Add(new Rectangle(1000,600,200,10));
        // Tak
        roof.Add(new Rectangle(400,340,300,10));
        roof.Add(new Rectangle(1000,490,200,10));
        // Väggar
        wall.Add(new Rectangle(200,310,100,390));
        wall.Add(new Rectangle(400,200,300,140));
        wall.Add(new Rectangle(800,350,100,200));
        // Blockd
        block.Add(new Rectangle(400, 510, 300, 190));
        block.Add(new Rectangle(400, 0, 300, 200));
        block.Add(new Rectangle(800, 160, 100, 190));
        block.Add(new Rectangle(800, 550, 100, 150));
        block.Add(new Rectangle(1000, 0, 200, 490));
        block.Add(new Rectangle(1000, 610, 200, 90));
        // Killfloor (Golv som TPar spelaren till början av leveln)
        killFloor.Add(new Rectangle(200,700,900,100));
        // Teleport
        teleport.Add(new Rectangle (1100,500, 100,100));
    }
}
