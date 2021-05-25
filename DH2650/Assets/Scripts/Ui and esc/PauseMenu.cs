using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverScreen;
    public GameObject ProgressBar;
    public LoadingBar loadingBar;
    public bool lockedMouse = true;
    public static bool AtSavePoint = false;
    public float PressingDuration = 2f;
    private float duration = 0f;
    private bool pressed = false;
    private GameObject resumeButton;
    private GameObject tryAgainButton;
    private CanvasGroup canvasGroup;
    private CanvasGroup gameOverCanvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        resumeButton = pauseMenuUI.transform.GetChild(0).gameObject;
        tryAgainButton = gameOverScreen.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }

        if (GameIsPaused || !AtSavePoint)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            pressed = true;
        }

        if (pressed)
        {
            if (!ProgressBar.activeSelf)
            {
                ProgressBar.SetActive(true);
            }
            duration += Time.deltaTime;
            loadingBar.SetProgress(duration / PressingDuration);
            if (duration > PressingDuration)
            {
                gameObject.GetComponent<SaveHelper>().SaveLevel();
                ProgressBar.SetActive(false);
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            duration = 0f;
            ProgressBar.SetActive(false);
            pressed = false;
        }
    }

    public void GameOverPause()
    {
        gameOverScreen.SetActive(true);
        if (gameOverCanvasGroup == null)
        {
            gameOverCanvasGroup = tryAgainButton.GetComponent<CanvasGroup>();
        }
        else
        {
            gameOverCanvasGroup.alpha = 1;
        }

        //Time.timeScale = 0f;
        GameIsPaused = true;
        if (lockedMouse)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void GameOverTryAgain()
    {
        gameOverScreen.SetActive(false);
        //Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameIsPaused = false;
        if (lockedMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        //Time.timeScale = 1f;
        GameIsPaused = false;
        if (lockedMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        if (canvasGroup == null)
        {
            canvasGroup = resumeButton.GetComponent<CanvasGroup>();
        } else
        {
            canvasGroup.alpha = 1;
        }
        
        //Time.timeScale = 0f;
        GameIsPaused = true;
        if (lockedMouse)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void LoadMenu()
    {
        //Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void LoadLevelHub()
    {
        //Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
