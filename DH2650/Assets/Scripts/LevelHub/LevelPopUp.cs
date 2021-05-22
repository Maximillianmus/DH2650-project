using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ricimi;
using UnityEngine.SceneManagement;

public class LevelPopUp : MonoBehaviour
{
    public GameObject popup;
    public TMP_Text title;
    public TMP_Text chestNum;
    public Ricimi.CleanButton trashBtn;
    public Ricimi.CleanButton playBtn;
    private float delay;
    public GameObject closeButton;
    private CanvasGroup canvasGroup;
    public LevelManager levelManager;

    void Start()
    {
        //title = popup.transform.Find("Title").GetComponent<TMP_Text>();
        //chestNum = GameObject.Find("ChestNum").GetComponent<TMP_Text>();
        //trashBtn = popup.transform.Find("TrashButton").GetComponent<Ricimi.CleanButton>();
        //closeButton = GameObject.Find("CloseButton");
    }

    // Update is called once per frame
    void Update()
    {
        if (!popup.activeSelf)
        {
            delay += Time.deltaTime;
        }
    }

    public bool IsActive()
    {
        if (delay < 0.5f)
        {
            return true;
        }
        return popup.activeSelf;
    }

    public void SetPopupActive(bool active)
    {
        if (canvasGroup == null)
        {
            canvasGroup = closeButton.GetComponent<CanvasGroup>();
        }
        else
        {
            canvasGroup.alpha = 1;
        }
        popup.SetActive(active);
        if (active) delay = 0;
    }

    public void UpdateLevelInfo(string levelName)
    {
        title.SetText(levelName);
        var score = LevelInfo.ScoresInfo[levelName][0];
        chestNum.SetText($"{score}");

        trashBtn.gameObject.SetActive(SaveSystem.LoadPlayer(levelName) != null);
        if (levelManager.SumScore < score)
        {
            playBtn.SetEnabled(false);
        } else
        {
            playBtn.SetEnabled(true);
        }
        SetPopupActive(true);
        Debug.Log($"Updated info to {levelName}");
    }

    public void RemoveDataFile()
    {
        string path = $"{Application.persistentDataPath}/{title.text}.yarrharr";
        Debug.Log($"Removed {path}");
    }

    public void LoadScene()
    {
        Debug.Log($"Load scene {title.text}");
        SceneManager.LoadScene(title.text);
    }
}
