using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChestBar : MonoBehaviour
{
    public GameObject Minbar;
    public GameObject SubBar;
    public LoadingBar progress;
    public Transform Parent;
    float totalScore;
    float minScore;
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
        minScore = (float)scores[0];
        totalScore = (float)scores[1];
        Minbar.GetComponent<MinScore>().SetBar(minScore / totalScore);

        for (float i = 1; i < totalScore; i++)
        {
            if (i == 1)
                SubBar.GetComponent<MinScore>().SetBar(i / totalScore);
            else
            {
                GameObject bar = Instantiate(SubBar, Parent) as GameObject;
                bar.GetComponent<MinScore>().SetBar(i / totalScore);
            }
        }
    }

    public void UpdateChestProgress(int count)
    {
        openCount = (float)count;
        progress.SetProgress(openCount / totalScore);
    }



    // Update is called once per frame
    public void UpdateOpenCount()
    {
        openCount += 1;
        progress.SetProgress(openCount / totalScore);
    }
}
