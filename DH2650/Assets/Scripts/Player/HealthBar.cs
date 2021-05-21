using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Don't change the health here! Change in 'PlayerHealth.cs'!")]
    public Image slider;

    public PlayerHealth playerHealth;

    public void SetHealth(float health)
    {
        float amount = health / playerHealth.maxHealth;
        slider.fillAmount = amount;

    }

    public void SetMaxHealth(float health)
    {
        slider.fillAmount = 1;
    }

}
