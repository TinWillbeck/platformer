using Raylib_cs;
using System.Numerics;

Raylib.InitWindow(1500,800, "platformer");
Raylib.SetTargetFPS(60);

Rectangle Player = new Rectangle(60,660, 60,65);
bool isTouching = false;

Level1 level1 = new();
Level2 level2 = new();
testlvl testlvl = new();

string currentScene = "start";

float speed = 10;
float jump = 120;
float gravity = 5;



while(Raylib.WindowShouldClose()==false)
{

    // Här ritas leveln ut och all collision kollas,dvs golv, tak, väggar samt teleport till nästa level
    
    Raylib.BeginDrawing();

    if (currentScene == "start"){
        Raylib.DrawText("Press ENTER to start", 550,300,32, Color.WHITE);
        Raylib.DrawText("Press ESC at any time to quit the game", 500,400,24, Color.WHITE);
        Raylib.ClearBackground(Color.BLACK);
    }
    else if (currentScene == "endScreen"){
        Raylib.DrawText("GG hoppas jag får godkänt!", 550,300,32,Color.WHITE);
        Raylib.ClearBackground(Color.BLACK);

    }
    else if (currentScene == "test"){
        Raylib.DrawRectangleRec(Player, Color.WHITE);
        DrawLevel(testlvl.structure, testlvl.teleport, testlvl.wall, testlvl.block, testlvl.roof,testlvl.killFloor);

        (Player, isTouching) = ActiveCollision(Player, isTouching, testlvl.structure, testlvl.wall, gravity, speed);
        Player = StaticCollision(Player, testlvl.block, testlvl.roof, speed);
        (Player, currentScene) = tpCollision(Player, testlvl.teleport, currentScene, "start", testlvl.killFloor);
    }
    else if(currentScene == "level1")
    {
        Raylib.DrawRectangleRec(Player, Color.WHITE);
        DrawLevel(level1.structure, level1.teleport, level1.wall, level1.block,level1.roof,level1.killFloor);
        Raylib.DrawText("Använd W, A, D för att flytta den vita kuben. Ta dig till gröna kuben för att gå till nästa nivå.", 100, 100, 26, Color.WHITE);

        (Player, isTouching) = ActiveCollision(Player, isTouching, level1.structure, level1.wall, gravity, speed);
        (Player, currentScene) = tpCollision(Player, level1.teleport, currentScene, "level2", level1.killFloor);
    }
    else if (currentScene == "level2")
    {
        Raylib.DrawRectangleRec(Player, Color.WHITE);
        DrawLevel(level2.structure, level2.teleport, level2.wall, level2.block, level2.roof,level2.killFloor);
        Raylib.DrawText("Håll A eller D mot lila väggar för att glida ner för dem.", 100, 100, 26, Color.WHITE);
        Raylib.DrawText("Klicka W när du är i kontakt med en lila vägg för att klättra upp för den", 100, 150, 26, Color.WHITE);
        Raylib.DrawText("Akta dig för lava!", 450, 725, 26, Color.WHITE);

        (Player, isTouching) = ActiveCollision(Player, isTouching, level2.structure, level2.wall, gravity, speed);
        Player = StaticCollision(Player, level2.block, level2.roof, speed);
        (Player, currentScene) = tpCollision(Player, level2.teleport, currentScene, "endScreen", level2.killFloor);
    }

    Raylib.EndDrawing();


    // Logik, i princip bara gubbens rörelse samt att man kan starta spelet

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

static void DrawLevel(List<Rectangle> structure, List<Rectangle> teleport, List<Rectangle> wall, List<Rectangle> block, List<Rectangle> roof, List<Rectangle> killFloor)
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
    Raylib.DrawRectangleRec(wall[i], Color.PURPLE);
    }
    for (var i = 0; i < block.Count; i++)
    {
        Raylib.DrawRectangleRec(block[i], Color.BLUE);
    }
    for (var i = 0; i < roof.Count; i++)
    {
        Raylib.DrawRectangleRec(roof[i], Color.GRAY);
    }
    for (var i = 0; i < killFloor.Count; i++)
    {
        Raylib.DrawRectangleRec(killFloor[i], Color.ORANGE);
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
    if (Raylib.IsKeyDown(KeyboardKey.KEY_R))
    {
        Player.x = 60;
        Player.y = 660;
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

static Rectangle StaticCollision(Rectangle Player, List<Rectangle> block, List<Rectangle> roof, float speed)
{
    for (var i = 0; i < block.Count; i++)
    {
        if (Raylib.CheckCollisionRecs(Player, block[i]))
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
    for (var i = 0; i < roof.Count; i++)
    {
        if (Raylib.CheckCollisionRecs(Player, roof[i]))
        {
            Player.y = roof[i].y + 5;
        }
    }

    return Player;
}

static (Rectangle, string) tpCollision(Rectangle Player, List<Rectangle> teleport, string currentScene, string nextScene, List<Rectangle> killFloor)
{
    if (Raylib.CheckCollisionRecs(Player, teleport[0]))
    {
        Player.x = 60;
        Player.y = 660;
        currentScene = nextScene;
        
    }

    for (var i = 0; i < killFloor.Count; i++)
    {
        if (Raylib.CheckCollisionRecs(Player, killFloor[i])){
            Player.x = 60;
            Player.y = 660;
        }
    }
    return(Player, currentScene);
}
