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

    public HealthBar healthbar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(currentHealth);
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
        if (currentHealth > 1)
        {
            currentHealth -= damage;
            healthbar.SetHealth(currentHealth);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        

    }

    public void LoadHealth(float health)
    {
        healthbar.SetHealth(health);
    }


}
