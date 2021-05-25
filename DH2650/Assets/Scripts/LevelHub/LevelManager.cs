using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    private LevelPoint[] levels;
    public TMP_Text mText;
    public int SumScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        levels = gameObject.GetComponentsInChildren<LevelPoint>();
        UpdateSum();
    }

    public void UpdateSum()
    {
        SumScore = 0;
        foreach (LevelPoint levelPoint in levels)
        {
            LevelData data = SaveSystem.LoadPlayer(levelPoint.SceneName);
            if (data != null)
                SumScore += data.Score;
        }
        mText.SetText($"{SumScore}");
    }

}
