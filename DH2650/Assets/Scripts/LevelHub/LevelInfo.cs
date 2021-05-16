using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo
{
    // int[2]: Min score to pass a scene, and full score. 
    public static Dictionary<string, int[]> ScoresInfo = new Dictionary<string, int[]>
    {
        {"Prototype-level v.2", new int[]{1, 4, } },
    };
}
