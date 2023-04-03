using System;
using Raylib_cs;


public class MovementClass
{
    
// Gör så spelaren kan röra på sig
public (Rectangle, float) Movement(Rectangle player, bool isTouching, float speed, float jump, float velocity, float gravity)
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
    
}
