using System;
using Raylib_cs;


public class StaticCollisionClass
{
        // Kollision där spelaren inte får förmågan att hoppa
    public (Rectangle,float) StaticCollision(Rectangle player, List<Rectangle> block, List<Rectangle> roof, float speed, float velocity)
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
                velocity = 1f;
            }
        }
        return (player, velocity);
    }
}
