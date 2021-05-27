using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    private LevelPoint[] levels;
    public GameObject congrats;
    public TMP_Text mText;
    public int SumScore = 0;
    private Dictionary<string, int> scores = new Dictionary<string, int>();

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
            {
                SumScore += data.Score;
                scores[levelPoint.SceneName] = data.Score;
            }
        }
        mText.SetText($"{SumScore}");

        if (LevelInfo.TotalScore() == SumScore)
        {
            congrats.SetActive(true);
        }
    }

    public int GetSceneScore(string sceneName)
    {
        return scores[sceneName];
    }

}
