using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    [Header("Set the max health here! The player HealthBar updates from here!")]

    public float maxHealth;
    public float currentHealth;
    private PauseMenu pauseMenu;

    public HealthBar healthbar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(currentHealth);
        var pauseMenuGameObject = GameObject.Find("PauseMenuCanvas");
        if (pauseMenuGameObject)
        {
            pauseMenu = pauseMenuGameObject.GetComponent<PauseMenu>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    /*
    * Public so enemy can access this function
    */
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth > 1)
        {
            healthbar.SetHealth(currentHealth);
        }
        else
        {
            healthbar.SetHealth(0);
            if (pauseMenu)
            {
                pauseMenu.GameOverPause();
            } else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

    }

    public void LoadHealth(float health)
    {
        healthbar.SetHealth(health);
    }


}
