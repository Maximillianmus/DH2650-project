using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class ChestBar : MonoBehaviour
{
    public TMP_Text mText;
    float totalScore;
    float openCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        string scene = SceneManager.GetActiveScene().name;
        Dictionary<string, int[]> scoresInfo = LevelInfo.ScoresInfo;
        if (!scoresInfo.ContainsKey(scene))
        {
            Debug.LogError($"{scene} not set");
            return;
        }
        int[] scores = scoresInfo[scene];
        totalScore = (float)scores[1];

        //mText = GameObject.Find("Text").GetComponent<TextMeshPro>();
        mText.SetText($"{openCount} / {totalScore}");
    }

    public void UpdateChestProgress(int count)
    {
        openCount = (float)count;
        mText.SetText($"{openCount} / {totalScore}");
    }



    // Update is called once per frame
    public void UpdateOpenCount()
    {
        openCount += 1;
        mText.SetText($"{openCount} / {totalScore}");
    }
}
