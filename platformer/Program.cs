using Raylib_cs;
using System.Numerics;

Raylib.InitWindow(1500,800, "platformer");
Raylib.SetTargetFPS(60);

Rectangle Player = new Rectangle(60,60, 60,60);
bool isTouching = false;

List<Rectangle> structure = new();

structure.Add(new Rectangle(0, 700, 1500, 100));

float speed = 10;
float jump = 200;
float gravity = 5;


while(Raylib.WindowShouldClose()==false)
{


    Raylib.BeginDrawing();
    Raylib.DrawRectangleRec(Player, Color.WHITE);
    level(structure);
    Raylib.EndDrawing();



    Player.y += gravity;

    if (!Raylib.CheckCollisionRecs(Player, structure[0]))
    {
        isTouching = false;
    }

    if (Raylib.CheckCollisionRecs(Player, structure[0] ))
    {
        Player.y = structure[0].y-Player.height;
        isTouching = true;
    }


    if (Raylib.IsKeyPressed(KeyboardKey.KEY_W) && isTouching == true)
    {
        Player.y -= jump;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
    {
        Player.x -= speed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
    {
        Player.x += speed;
    }


}

static void level(List<Rectangle> structure)
{
    Raylib.ClearBackground(Color.BLACK);
    Raylib.DrawRectangleRec(structure[0], Color.RED);
}