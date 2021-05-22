using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    private LevelPoint[] levels;
    public TMP_Text mText;
    private float distanceThreshold = 100;
    public int SumScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        levels = gameObject.GetComponentsInChildren<LevelPoint>();
        foreach (LevelPoint levelPoint in levels)
        {
            LevelData data = SaveSystem.LoadPlayer(levelPoint.SceneName);
            if (data != null)
                SumScore += data.Score;
        }
        mText.SetText($"{SumScore}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public LevelPoint GetClosestLevel(Transform transform)
    {
        float minDistance = 999;
        LevelPoint closest = null;
        foreach (LevelPoint levelPoint in levels)
        {
            float distance = Vector3.Distance(levelPoint.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = levelPoint;
            }
        }

        if (minDistance < distanceThreshold)
        {
            return closest;
        }
        return null;
    }
}
