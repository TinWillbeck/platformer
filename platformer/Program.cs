using Raylib_cs;
using System.Numerics;

Raylib.InitWindow(1500,800, "platformer");
Raylib.SetTargetFPS(60);

Rectangle Player = new Rectangle(60,660, 60,60);
bool isTouching = false;

Level1 level = new();


string currentScene = "level1";

float speed = 10;
float jump = 120;
float gravity = 5;



while(Raylib.WindowShouldClose()==false)
{

    // Grafik

    Raylib.BeginDrawing();

    if (currentScene == "start"){
        Raylib.DrawText("Press ENTER to start", 550,300,32, Color.WHITE);
    }

    else if(currentScene == "level1"){
    Raylib.DrawRectangleRec(Player, Color.WHITE);
    Level(level.structure, level.teleport);
    }


    Raylib.EndDrawing();


    // Logik
    if (currentScene == "start"){
        if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
        {
            currentScene = "level1";
        }
    }
    else
    {

        Player.y += gravity;

        isTouching = false;
        for (var i = 0; i < level.structure.Count; i++)
        {
            if (Raylib.CheckCollisionRecs(Player, level.structure[i]))
            {
                Player.y = level.structure[i].y - Player.height;
                isTouching = true;
            }
            if (Raylib.CheckCollisionRecs(Player, level.teleport[0]))
            {
                // currentScene = 
            }
        }

        Player = Movement(Player, isTouching, speed, jump);
    }

}

static void Level(List<Rectangle> structure, List<Rectangle> teleport)
{
    
    Raylib.ClearBackground(Color.BLACK);
    Raylib.DrawText("Använd W, A, D för att flytta den vita kuben. Ta dig till gröna kuben för att gå till nästa nivå.", 100,100,26, Color.WHITE);
    for (var i = 0; i < structure.Count; i++)
    {
        Raylib.DrawRectangleRec(structure[i], Color.RED);
    }
    for (var i = 0; i < teleport.Count; i++)
    {
        Raylib.DrawRectangleRec(teleport[i], Color.GREEN);
    }
}

static Rectangle Movement(Rectangle Player, bool isTouching, float speed, float jump)
{
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

    if (Player.x <= -10)
    {
        Player.x += speed;
    }
    if (Player.x >= 1450)
    {
        Player.x -= speed;
    }

    return Player;
}