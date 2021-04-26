using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [Header("Don't change the health here!", order = 0)]
    [Space(-10, order = 1)]
    [Header("Change player health in PlayerHealth script!", order = 2)]

    public Slider slider;
    public Text text;

    public PlayerHealth playerHealth;

    public void SetHealth(float health)
    {
        slider.value = health;

        text.text = health.ToString() + "/" + playerHealth.maxHealth.ToString();
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        text.text = playerHealth.maxHealth.ToString() + "/" + playerHealth.maxHealth.ToString();
    }

}
