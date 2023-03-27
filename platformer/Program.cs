using Raylib_cs;

Raylib.InitWindow(1500,800, "platformer");
Raylib.SetTargetFPS(60);

Rectangle player = new Rectangle(60,660, 60,65);
bool isTouching = false;

testlvl testlvl = new();
Level1 level1 = new();
Level2 level2 = new();
Level3 level3 = new();

string currentScene = "level2";

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
        Raylib.ClearBackground(Color.BLACK);

    }

    // Rita test level som jag använde för att prova fysiken bland annat
    else if (currentScene == "test"){
        Raylib.DrawRectangleRec(player, Color.WHITE);
        DrawLevel(testlvl.structure, testlvl.teleport, testlvl.wall, testlvl.block, testlvl.roof,testlvl.killFloor);

        (player, isTouching) = ActiveCollision(player, isTouching, testlvl.structure, testlvl.wall, gravity, speed, velocity);
        (player, velocity) = StaticCollision(player, testlvl.block, testlvl.roof, speed, velocity);
        (player, currentScene) = tpCollision(player, testlvl.teleport, currentScene, "start", testlvl.killFloor);
    }

    // Rita level 1
    else if(currentScene == "level1")
    {
        
        Raylib.DrawRectangleRec(player, Color.WHITE);
        
        DrawLevel(level1.structure, level1.teleport, level1.wall, level1.block,level1.roof,level1.killFloor);
        Raylib.DrawText("Använd W/space, A, D för att flytta den vita kuben.", 100, 100, 26, Color.WHITE);
        Raylib.DrawText("Ta dig till gröna kuben för att gå till nästa nivå.", 110, 150, 26, Color.WHITE);

        (player, isTouching) = ActiveCollision(player, isTouching, level1.structure, level1.wall, gravity, speed, velocity);
        (player, currentScene) = tpCollision(player, level1.teleport, currentScene, "level2", level1.killFloor);
    }

    // Rita level 2
    else if (currentScene == "level2")
    {
        Raylib.DrawRectangleRec(player, Color.WHITE);
        DrawLevel(level2.structure, level2.teleport, level2.wall, level2.block, level2.roof,level2.killFloor);
        Raylib.DrawText("Håll A eller D mot lila väggar för att glida ner för dem.", 100, 100, 26, Color.WHITE);
        Raylib.DrawText("Hoppa när du är i kontakt med en lila vägg för att klättra upp för den", 100, 150, 26, Color.WHITE);

        Raylib.DrawText("Akta dig för lava!", 450, 725, 26, Color.WHITE);
        (player, isTouching) = ActiveCollision(player, isTouching, level2.structure, level2.wall, gravity, speed, velocity);
        (player, velocity) = StaticCollision(player, level2.block, level2.roof, speed, velocity);
        (player, currentScene) = tpCollision(player, level2.teleport, currentScene, "endScreen", level2.killFloor);
    }
    // Rita level 3
    else if (currentScene == "level3")
    {
        Raylib.DrawRectangleRec(player,Color.WHITE);
        DrawLevel(level3.structure, level3.teleport, level3.wall,level3.block,level3.roof,level3.killFloor);

        (player, isTouching) = ActiveCollision(player, isTouching, level3.structure, level3.wall, gravity, speed, velocity);
        (player, velocity) = StaticCollision(player, level3.block, level3.roof, speed, velocity);
        (player, currentScene) = tpCollision(player, level3.teleport, currentScene, "endScreen", level3.killFloor);
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
        (player, velocity) = Movement(player, isTouching, speed, jump, velocity, gravity);
    }

    
}


// metoder

// IDE FÖR MJUKARE HOPP
// Gör så gravitationen ökar exponentiellt när karaktären är i luften
// Gör så man får en spurt uppåt när man trycker på W/ space(vector2 på nnågot vis)


// Ritar ut alla objekt förutom spelaren
static void DrawLevel(List<Rectangle> structure, List<Rectangle> teleport, List<Rectangle> wall, List<Rectangle> block, List<Rectangle> roof, List<Rectangle> killFloor)
{
    // rita bakgrund
    Raylib.ClearBackground(Color.BLACK);
    // ritar ut alla objekt av olika typ resten av metoden
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

// Aktiv kollision som jag har kallat det innebär att när spelaren nuddar detta objekt så har den förmågan att hoppa. 
// Detta kollas med boolen isTouching som är sann om spelaren nuddar något av objekten i listan.

static (Rectangle, bool) ActiveCollision(Rectangle player, bool isTouching, List<Rectangle> structure, List<Rectangle> wall, float gravity, float speed, float velocity)
{
    isTouching = false;
// Kollisionen i övrigt fungerar så att om spelaren och objektet nuddar varandra så ändras dens position på något vis.
// För golv så blir spelarens Y koordinat minskad med spelarens höjd + 5 pixlar(eftersom spelaren är 65px lång så blir den en jämn och fin kub när den nuddar marken)

    for (var i = 0; i < structure.Count; i++)
    {

        if (Raylib.CheckCollisionRecs(player, structure[i]))
        {
            isTouching = true;
            player.y = structure[i].y - player.height+5;
        }
    }
// För väggar så blir spelarens X koordinat minskad eller ökad med speed beroende på vilken tangent man håller in
// När man håller in A mot en vägg så kommer spelarens x öka med speed, vilket motverkar hur den normalt rör på sig. Motsatt för D.
// När man dessutom är i kontakt med väggen så fallen spelaren långsammare.
    for (int i = 0; i < wall.Count; i++)
    {
        if (Raylib.CheckCollisionRecs(player, wall[i]))
        {
            isTouching = true;
            player.y -= gravity - 2;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
            {
                player.x += speed;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
            {
                player.x -= speed;
            }
        }
    }

    return (player, isTouching);
}

// Kollision där spelaren inte får förmågan att hoppa
static (Rectangle,float) StaticCollision(Rectangle player, List<Rectangle> block, List<Rectangle> roof, float speed, float velocity)
{
    // Kollision för väggar
    for (var i = 0; i < block.Count; i++)
    {
        if (Raylib.CheckCollisionRecs(player, block[i]))
        {
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
            {
                player.x += speed;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
            {
                player.x -= speed;
            }
        }
    }
    // Kollision för tak
    for (var i = 0; i < roof.Count; i++)
    {
        if (Raylib.CheckCollisionRecs(player, roof[i]))
        {
            player.y = roof[i].y + 10;
            velocity = 0;
        }
    }
    return (player, velocity);
}

// Kollision som TPar spelaren till en annan plats, nästa level eller startpunkten
static (Rectangle, string) tpCollision(Rectangle player, List<Rectangle> teleport, string currentScene, string nextScene, List<Rectangle> killFloor)
{
    // TP till nästa level
    if (Raylib.CheckCollisionRecs(player, teleport[0]))
    {
        player.x = 60;
        player.y = 660;
        currentScene = nextScene;
        
    }

    // TP till början av leveln
    for (var i = 0; i < killFloor.Count; i++)
    {
        if (Raylib.CheckCollisionRecs(player, killFloor[i])){
            player.x = 60;
            player.y = 660;
        }
    }
    return(player, currentScene);
}
