using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SaveHelper : MonoBehaviour
{
    public GameObject Player;
    private string _sceneName;
    private LevelData _levelData;
    public AudioClip SaveSound;
    private AudioSource source;
    [SerializeField] Chest[] chests = new Chest[0];

    void Start()
    {
        _sceneName = SceneManager.GetActiveScene().name;
        source = GetComponent<AudioSource>();
        if (_sceneName != "LevelHub")
        {
            LoadLevel();
        }
    }

    public void SaveLevel(string sceneName="")
    {
        if (sceneName == "")
        {
            sceneName = _sceneName;
        }
        bool[] status = (from chest in chests select chest.IsOpen).ToArray(); ; // chests.ConvertAll<bool>(chest => chest.IsOpen);
        SaveSystem.SavePlayer(Player, status, sceneName);
        source.PlayOneShot(SaveSound);
    }

    public void LoadLevel(string sceneName="")
    {
        if (sceneName == "")
        {
            sceneName = _sceneName;
        }

        LevelData levelData;
        if (_levelData == null)
        {
            levelData = SaveSystem.LoadPlayer(sceneName);
            _levelData = levelData;
        } else
        {
            levelData = _levelData;
        }
        if (levelData == null) return;

        StartCoroutine(updateStatus());

    }

    IEnumerator updateStatus()
    {
        yield return new WaitForSeconds(0.01f);
        PlayerHealth playerHealth = Player.GetComponent<PlayerHealth>();
        playerHealth.LoadHealth(_levelData.Health);

        Player.transform.position = new Vector3(
            _levelData.Position[0],
            _levelData.Position[1],
            _levelData.Position[2]
        );

        for (int i = 0; i < chests.Length; i++)
        {
            if (_levelData.ChestsOpenStatus[i])
                chests[i].SetOpen();
        }
    }

    public int LoadScore(string sceneName="")
    {
        if (sceneName == "")
        {
            sceneName = _sceneName;
        }

        LevelData levelData;
        if (_levelData == null)
        {
            levelData = SaveSystem.LoadPlayer(sceneName);
            _levelData = levelData;
        }
        else
        {
            levelData = _levelData;
        }
        if (levelData == null) return 0;

        return levelData.Score;
    }
}
