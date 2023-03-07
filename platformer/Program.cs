using Raylib_cs;
using System.Numerics;

Raylib.InitWindow(1500,800, "platformer");
Raylib.SetTargetFPS(60);

Rectangle Player = new Rectangle(60,60, 60,60);

List<Rectangle> structure = new();


float speed = 10;
float gravity = 5;


while(Raylib.WindowShouldClose()==false)
{

    Player.y += gravity;

    if (Raylib.IsKeyDown(KeyboardKey.KEY_W)){
        Player.y -= speed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_A)){
        Player.x -= speed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_S)){
        Player.y += speed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D)){
        Player.x += speed;
    }

    structure.Add(new Rectangle(200,200,200,200));


    


    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.BLACK);
    Raylib.DrawRectangleRec(Player, Color.WHITE);
    Raylib.DrawRectangleRec(structure[0], Color.RED);
    Raylib.EndDrawing();
}