using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Text text;

    public PlayerHealth playerHealth;

    public void SetHealth(int health)
    {
        slider.value = health;

        text.text = health.ToString() + "/" + playerHealth.maxHealth.ToString();
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        text.text = playerHealth.maxHealth.ToString() + "/" + playerHealth.maxHealth.ToString();
    }

    public float getMaxHealth()
    {
        return slider.maxValue;
    }
}