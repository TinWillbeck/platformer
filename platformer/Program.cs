using Raylib_cs;
using System.Numerics;

Raylib.InitWindow(1500,800, "platformer");
Raylib.SetTargetFPS(60);

Rectangle Player = new Rectangle(60,660, 60,65);
bool isTouching = false;

Level1 level1 = new();
Level2 level2 = new();

string currentScene = "level1";
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
        DrawLevel(level1.structure, level1.teleport, level1.wall, level1.block,level1.roof);

        (Player, isTouching) = ActiveCollision(Player, isTouching, level1.structure, level1.wall, gravity, speed);
        (Player, currentScene) = SceneSwitch(Player, level1.teleport, currentScene, "level2");
    }

    else if (currentScene == "level2")
    {
        Raylib.DrawRectangleRec(Player, Color.WHITE);
        Raylib.DrawText("Håll A eller D mot blåa väggar för att glida ner för dem.", 100, 100, 26, Color.WHITE);
        DrawLevel(level2.structure, level2.teleport, level2.wall, level2.block, level2.roof);

        (Player, isTouching) = ActiveCollision(Player, isTouching, level2.structure, level2.wall, gravity, speed);
        Player = StaticCollision(Player, level2, speed);
        // collisionWall(ref Player, ref isTouching, level2.wall, speed, gravity);

        (Player, currentScene) = SceneSwitch(Player, level2.teleport, currentScene, "endScreen");

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

static void DrawLevel(List<Rectangle> structure, List<Rectangle> teleport, List<Rectangle> wall, List<Rectangle> block, List<Rectangle> roof)
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
    for (var i = 0; i < block.Count; i++)
    {
        Raylib.DrawRectangleRec(block[i], Color.ORANGE);
    }
    for (var i = 0; i < roof.Count; i++)
    {
        Raylib.DrawRectangleRec(roof[i], Color.GRAY);
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

static (Rectangle, bool) ActiveCollision(Rectangle Player, bool isTouching, List<Rectangle> structure, List<Rectangle> wall, float gravity, float speed)
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

    return (Player, isTouching);
}

static Rectangle StaticCollision(Rectangle Player, Level2 level2, float speed)
{
    for (var i = 0; i < level2.block.Count; i++)
    {
        if (Raylib.CheckCollisionRecs(Player, level2.block[i]))
        {
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
    for (var i = 0; i < level2.roof.Count; i++)
    {
        if (Raylib.CheckCollisionRecs(Player, level2.roof[i]))
        {
            Player.y = level2.roof[i].y + 5;
        }
    }

    return Player;
}

// static void collisionWall(ref Rectangle Player, ref bool isTouching, List<Rectangle> wall, float speed, float gravity)
// {
//     for (int i = 0; i < wall.Count; i++)
//     {
//         if (Raylib.CheckCollisionRecs(Player, wall[i]))
//         {
//             isTouching = true;
//             Player.y -= gravity - 2;
//             if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
//             {
//                 Player.x += speed;
//             }
//             if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
//             {
//                 Player.x -= speed;
//             }
//         }
//     }
// }

static (Rectangle, string) SceneSwitch(Rectangle Player, List<Rectangle> teleport, string currentScene, string nextScene)
{
    if (Raylib.CheckCollisionRecs(Player, teleport[0]))
    {
        Player.x = 60;
        Player.y = 660;
        currentScene = nextScene;
    }
    return(Player, currentScene);
}
