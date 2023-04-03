using System;
using Raylib_cs;

public class ActiveCollisionClass
{
    // Aktiv kollision som jag har kallat det innebär att när spelaren nuddar detta objekt så har den förmågan att hoppa. 
    // Detta kollas med boolen isTouching som är sann om spelaren nuddar något av objekten i listan.
    public (Rectangle, bool) ActiveCollision(Rectangle player, bool isTouching, List<Rectangle> structure, List<Rectangle> wall, float gravity, float speed, float velocity)
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
}
