using Raylib_cs;
using System.Numerics;

Raylib.InitWindow(1500,800, "platformer");
Raylib.SetTargetFPS(60);

Rectangle Player = new Rectangle(60,660, 60,65);
bool isTouching = false;

Level1 level1 = new();
Level2 level2 = new();

string currentScene = "start";
string nextScene = "";

float speed = 10;
float jump = 120;
float gravity = 5;



while(Raylib.WindowShouldClose()==false)
{

    // Level init
    
    Raylib.BeginDrawing();

    if (currentScene == "start"){
        Raylib.DrawText("Press ENTER to start", 550,300,32, Color.WHITE);
    }
    else if (currentScene == "endScreen"){
        Raylib.DrawText("GG hoppas jag får godkänt!", 550,300,32,Color.WHITE);
    }
    else if(currentScene == "level1")
    {
        Raylib.DrawRectangleRec(Player, Color.WHITE);
        Raylib.DrawText("Använd W, A, D för att flytta den vita kuben. Ta dig till gröna kuben för att gå till nästa nivå.", 100, 100, 26, Color.WHITE);

        collisionFloor(ref Player, ref isTouching, level1.structure);
        Level(level1.structure, level1.teleport, level1.wall);
        sceneSwitch(ref Player, level1.teleport, ref currentScene, "level2");
    }

    else if (currentScene == "level2")
    {
        Raylib.DrawRectangleRec(Player, Color.WHITE);

        collisionFloor(ref Player, ref isTouching, level2.structure);
        Level(level2.structure, level2.teleport, level2.wall);
        collisionWall(ref Player, ref isTouching, level2.wall, speed, gravity);
        sceneSwitch(ref Player, level2.teleport, ref currentScene, "endScreen");

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

        Player = Movement(Player, isTouching, speed, jump);
    }

}

// metoder

static void Level(List<Rectangle> structure, List<Rectangle> teleport, List<Rectangle> wall)
{
    
    Raylib.ClearBackground(Color.BLACK);
    for (var i = 0; i < structure.Count; i++)
    {
        Raylib.DrawRectangleRec(structure[i], Color.RED);
    }
    for (var i = 0; i < teleport.Count; i++)
    {
        Raylib.DrawRectangleRec(teleport[i], Color.GREEN);
    }
    for (int i = 0; i < wall.Count; i++)
    {
        Raylib.DrawRectangleRec(wall[i], Color.BLUE);
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

static void collisionFloor(ref Rectangle Player, ref bool isTouching, List<Rectangle> structure)
{
    isTouching = false;
    for (var i = 0; i < structure.Count; i++)
    {

        if (Raylib.CheckCollisionRecs(Player, structure[i]))
        {
            Player.y = structure[i].y - Player.height;
            isTouching = true;
        }

    }

}

static void collisionWall(ref Rectangle Player, ref bool isTouching, List<Rectangle> wall, float speed, float gravity)
{
    for (int i = 0; i < wall.Count; i++)
    {
        if (Raylib.CheckCollisionRecs(Player, wall[i]))
        {
            isTouching = true;
            Player.y -= gravity - 2;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
            {
                Player.x += speed;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
            {
                Player.x -= speed;
            }
        }
    }
}

static void sceneSwitch(ref Rectangle Player, List<Rectangle> teleport, ref string currentScene, string nextScene)
{
    if (Raylib.CheckCollisionRecs(Player, teleport[0]))
    {
        Player.x = 60;
        Player.y = 660;
        currentScene = nextScene;
    }
}