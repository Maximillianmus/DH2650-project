using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject guidePanel;
    public void PlayGame()
    {
        SceneManager.LoadScene("LevelHub");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EnterGuide()
    {
        guidePanel.SetActive(true);
    }

    public void ExitGuide()
    {
        guidePanel.SetActive(false);
    }

    public void OpenWebsite()
    {
        Application.OpenURL("https://orrhsu.wixsite.com/yarr-harr");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
