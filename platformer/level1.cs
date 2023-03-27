using System;
using Raylib_cs;

public class Level1
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
    
    public Level1()
    {
        // Golvet
        structure.Add(new Rectangle(0, 700, 1500, 100)); 
        // Plattformer
        structure.Add(new Rectangle(200, 600, 200, 10));
        structure.Add(new Rectangle(400, 500, 200, 10));
        structure.Add(new Rectangle(600, 400, 200, 10));
        structure.Add(new Rectangle(800, 300, 450, 10));
        //Tak
        roof.Add(new Rectangle(200, 610, 200, 10)); 
        roof.Add(new Rectangle(400, 510,200,10)); 
        roof.Add(new Rectangle(600,410,200,10)); 
        roof.Add(new Rectangle(800,310,450,10)); 
        // Teleport
        teleport.Add(new Rectangle (1300,100, 100,100));
    }
    

}
