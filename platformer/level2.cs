using System;
using Raylib_cs;

public class Level2
{
    // Använder mig av listor för att jag vet ej hur många ojbekt jag vill ha i varje level, darför fungerar lisot bättre

    public List<Rectangle> structure = new();
    public List<Rectangle> teleport = new();
    public List<Rectangle> wall = new();
    public List <Rectangle> block = new();
    public List <Rectangle> roof = new();
    public List <Rectangle> killFloor = new();



    public Level2()
    {
        // Golvet
        structure.Add(new Rectangle(0, 700, 1500, 100)); 

        // Plattformer

        // Tak

        // Väggar

        // Block

        // Killfloor (Golv som TPar spelaren till början av leveln)

        // Teleport
        teleport.Add(new Rectangle (1300,600, 100,100));
    }

}
