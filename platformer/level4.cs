using System;
using Raylib_cs;

public class Level4
{
    // Använder mig av listor för att jag vet ej hur många ojbekt jag vill ha i varje level, darför fungerar lisot bättre

    // Golv
    public List<Rectangle> structure = new();
    // Teleports
    public List<Rectangle> teleport = new();
    // Väggar man kan klättra på
    public List<Rectangle> wall = new();
    // Block, Väggar man inte kan klättra på
    public List <Rectangle> block = new();
    // Tak
    public List <Rectangle> roof = new();
    // Golv man dör på
    public List <Rectangle> killFloor = new();

    public Level4(){
        // Golvet
        structure.Add(new Rectangle(0, 700, 200, 100)); 
        structure.Add(new Rectangle(800, 700, 700, 100)); 
        // Plattformer
        structure.Add(new Rectangle(1300, 200, 100,10));
        structure.Add(new Rectangle(900, 600, 100,10));
        structure.Add(new Rectangle(600, 500, 100,10));
        // Tak

        // Väggar

        // Block
        block.Add(new Rectangle(1300,210,100,490));
        // Killfloor (Golv som TPar spelaren till början av leveln)
         killFloor.Add(new Rectangle(200,750,600,100));       
        // Teleport
        teleport.Add(new Rectangle (1400,600, 100,100));
    }
}