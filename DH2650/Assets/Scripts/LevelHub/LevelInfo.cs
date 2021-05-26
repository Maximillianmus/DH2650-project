using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo
{
    // int[2]: Min score to unlock a scene, and full score. 
    public static Dictionary<string, int[]> ScoresInfo = new Dictionary<string, int[]>
    {
        {"Tutorial", new int[]{0, 3, } },
        {"SkyLevel", new int[]{0, 3, } },
        {"Cave", new int[]{0, 6, } },
        {"WaterLevel", new int[]{0, 5, } },
    };

    public static int TotalScore()
    {
        int total = 0;
        foreach (var scene in ScoresInfo.Keys)
        {
            var scores = ScoresInfo[scene];
            total += scores[1];
        }
        return total;
    }
}
