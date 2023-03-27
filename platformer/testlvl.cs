using System;
using Raylib_cs;

public class testlvl
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

    public testlvl(){
        // Golvet
        structure.Add(new Rectangle(0, 700, 900, 100)); 
        structure.Add(new Rectangle(1000, 700, 500, 100)); 
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
        // Killfloor (Golv som TPar spelaren till början av leveln)
        killFloor.Add(new Rectangle(900, 700, 100, 100));
        // Teleport
        teleport.Add(new Rectangle (1300,600, 100,100));
    }
}

// Level template:

// using System;
// using Raylib_cs;

// public class testlvl
// {
//     // Använder mig av listor för att jag vet ej hur många ojbekt jag vill ha i varje level, darför fungerar lisot bättre

//     // Golv
//     public List<Rectangle> structure = new();
//     // Teleports
//     public List<Rectangle> teleport = new();
//     // Väggar man kan klättra på
//     public List<Rectangle> wall = new();
//     // Väggar man inte kan klättra på
//     public List <Rectangle> block = new();
//     // Tak
//     public List <Rectangle> roof = new();
//     // Golv man dör på
//     public List <Rectangle> killFloor = new();

//     public testlvl(){
//         // Golvet
//
//         // Plattformer
//
//         // Tak
// 
//         // Väggar
//
//         // Block
//
//         // Killfloor (Golv som TPar spelaren till början av leveln)
//
//         // Teleport
//         teleport.Add(new Rectangle (1400,600, 100,100));
//     }
// }