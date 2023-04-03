using Raylib_cs;

// öppna förnstrer och sätt fps
Raylib.InitWindow(1500,800, "platformer");
Raylib.SetTargetFPS(60);

// skapa spelaren och isTouching
Rectangle player = new Rectangle(60,660, 60,65);
bool isTouching = false;

// skapa instanser av alla klasser
testlvl testlvl = new();
Level1 level1 = new();
Level2 level2 = new();
Level3 level3 = new();
DrawLevelClass drawlevel = new();
ActiveCollisionClass activeCollision = new();
TpCollisionClass tpCollision = new();
StaticCollisionClass staticCollision = new();

// skapa currenScene och sätt till start
string currentScene = "start";

// skapa alla variabler som har med spelarens rörelse att göra
float speed = 10;
float jump = -8.5f;
float velocity = 0;
float gravity = 0.3f;


while(Raylib.WindowShouldClose()==false)
{

    // Här ritas leveln ut och all collision kollas,dvs golv, tak, väggar samt teleport till nästa level.
    // I varje level finns bakgrunden, spelaren, utritade objekt samt kollision för objekten.

    Raylib.BeginDrawing();

    // Rita start scen
    if (currentScene == "start"){
        Raylib.DrawText("Press ENTER to start", 550,300,32, Color.WHITE);
        Raylib.DrawText("Press ESC at any time to quit the game", 500,400,24, Color.WHITE);
        Raylib.ClearBackground(Color.BLACK);
    }

    // Rita slut scen
    else if (currentScene == "endScreen"){
        Raylib.DrawText("GG hoppas jag får godkänt!", 550,300,32,Color.WHITE);
        Raylib.DrawText("Klicka på ENTER för att starta om", 555, 400, 24, Color.WHITE);
        Raylib.ClearBackground(Color.BLACK);

    }

    // Rita test level som jag använde för att prova fysiken bland annat
    else if (currentScene == "test"){
        // rita ut spelaren, leveln och eventuell text
        Raylib.DrawRectangleRec(player, Color.WHITE);
        drawlevel.DrawLevel(testlvl.structure, testlvl.teleport, testlvl.wall, testlvl.block, testlvl.roof,testlvl.killFloor);

        // kolla efter kollision
        (player, isTouching) = activeCollision.ActiveCollision(player, isTouching, testlvl.structure, testlvl.wall, gravity, speed, velocity);
        (player, velocity) = staticCollision.StaticCollision(player, testlvl.block, testlvl.roof, speed, velocity);
        (player, currentScene) = tpCollision.TpCollision(player, testlvl.teleport, currentScene, "start", testlvl.killFloor);
    }

    // Rita level 1
    else if(currentScene == "level1")
    {
        
        Raylib.DrawRectangleRec(player, Color.WHITE);
        
        drawlevel.DrawLevel(level1.structure, level1.teleport, level1.wall, level1.block,level1.roof,level1.killFloor);
        Raylib.DrawText("Använd W/space, A, D för att flytta den vita kuben.", 100, 100, 26, Color.WHITE);
        Raylib.DrawText("Ta dig till gröna kuben för att gå till nästa nivå.", 110, 150, 26, Color.WHITE);

        (player, velocity) = staticCollision.StaticCollision(player, level1.block, level1.roof, speed, velocity);
        (player, isTouching) = activeCollision.ActiveCollision(player, isTouching, level1.structure, level1.wall, gravity, speed, velocity);
        (player, currentScene) = tpCollision.TpCollision(player, level1.teleport, currentScene, "level2", level1.killFloor);
    }

    // Rita level 2
    else if (currentScene == "level2")
    {
        Raylib.DrawRectangleRec(player, Color.WHITE);
        drawlevel.DrawLevel(level2.structure, level2.teleport, level2.wall, level2.block, level2.roof,level2.killFloor);
        Raylib.DrawText("Håll A eller D mot lila väggar för att glida ner för dem.", 100, 100, 26, Color.WHITE);
        Raylib.DrawText("Hoppa när du är i kontakt med en lila vägg för att klättra upp för den", 100, 150, 26, Color.WHITE);
        Raylib.DrawText("Akta dig för lava!", 900, 725, 26, Color.WHITE);

        (player, isTouching) = activeCollision.ActiveCollision(player, isTouching, level2.structure, level2.wall, gravity, speed, velocity);
        (player, velocity) = staticCollision.StaticCollision(player, level2.block, level2.roof, speed, velocity);
        (player, currentScene) = tpCollision.TpCollision(player, level2.teleport, currentScene, "level3", level2.killFloor);
    }
    // Rita level 3
    else if (currentScene == "level3")
    {
        Raylib.DrawRectangleRec(player,Color.WHITE);
        drawlevel.DrawLevel(level3.structure, level3.teleport, level3.wall,level3.block,level3.roof,level3.killFloor);

        (player, isTouching) = activeCollision.ActiveCollision(player, isTouching, level3.structure, level3.wall, gravity, speed, velocity);
        (player, velocity) = staticCollision.StaticCollision(player, level3.block, level3.roof, speed, velocity);
        (player, currentScene) = tpCollision.TpCollision(player, level3.teleport, currentScene, "endScreen", level3.killFloor);
    }

    Raylib.EndDrawing();

    // Logik, i princip bara gubbens rörelse samt att man kan starta spelet

    if (currentScene == "start"){
        if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
        {
            currentScene = "level1";
        }
    }
    else if (currentScene == "endScreen"){
        if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
        {
            currentScene = "start";
        }
    }
    else
    {
        (player, velocity) = Movement(player, isTouching, speed, jump, velocity, gravity);
    }

    
}


// metoder

// IDE FÖR MJUKARE HOPP
// Gör så gravitationen ökar exponentiellt när karaktären är i luften
// Gör så man får en spurt uppåt när man trycker på W/ space(vector2 på nnågot vis)




// Gör så spelaren kan röra på sig
static (Rectangle, float) Movement(Rectangle player, bool isTouching, float speed, float jump, float velocity, float gravity)
{

    // gör så spelaren kan hoppa om den trycker på W/space
    // gör direkt så isTouching är falskt för att annars fungerade det inte utan att spelaren TPades uppåt.
    if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && isTouching == true || Raylib.IsKeyDown(KeyboardKey.KEY_SPACE) && isTouching == true)
    {   
        isTouching = false;
        velocity = jump;
    }
    // spelaren kan gå åt vänster
    if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
    {
        player.x -= speed;
    }
    // spelaren kan gå åt höger
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
    {
        player.x += speed;
    }
    // spelaren kan TPa till början av leveln
    if (Raylib.IsKeyDown(KeyboardKey.KEY_R))
    {
        player.x = 60;
        player.y = 660;
    }

    // Gör hoppen mjukare
    velocity += gravity;
    player.y += velocity;
    if (isTouching == true)
    {
        velocity = 0;
    }

    // gör så spelaren inte kan gå utanför leveln
    if (player.x <= -10)
    {
        player.x += speed;
    }
    if (player.x >= 1450)
    {
        player.x -= speed;
    }

    return (player, velocity);
}
