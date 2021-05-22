using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo
{
    // int[2]: Min score to unlock a scene, and full score. 
    public static Dictionary<string, int[]> ScoresInfo = new Dictionary<string, int[]>
    {
        {"Tutorial", new int[]{1, 4, } },
        {"SkyLevel", new int[]{1, 4, } },
        {"Cave", new int[]{1, 4, } },
    };
}
