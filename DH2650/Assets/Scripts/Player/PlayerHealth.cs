using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public float maxHealth = 20;
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
        currentHealth -= damage;

        if (currentHealth > 1)
        {
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
