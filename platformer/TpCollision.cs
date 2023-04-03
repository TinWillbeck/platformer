using System;
using Raylib_cs;

public class TpCollisionClass
{
    // Kollision som TPar spelaren till en annan plats, nästa level eller startpunkten
    public (Rectangle, string) TpCollision(Rectangle player, List<Rectangle> teleport, string currentScene, string nextScene, List<Rectangle> killFloor)
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

}
