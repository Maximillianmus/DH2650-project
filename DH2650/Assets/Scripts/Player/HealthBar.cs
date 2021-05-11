using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Don't change the health here! Change in 'PlayerHealth.cs'!")]
    public Image slider;
    //public Text text;

    public PlayerHealth playerHealth;

    public void SetHealth(float health)
    {
        float amount = health / playerHealth.maxHealth;
        slider.fillAmount = amount;

        //text.text = Mathf.Ceil(health).ToString() + "/" + playerHealth.maxHealth.ToString();
    }

    public void SetMaxHealth(float health)
    {
        slider.fillAmount = 1;
        //slider.maxValue = health;
        //slider.value = health;

        //text.text = playerHealth.maxHealth.ToString() + "/" + playerHealth.maxHealth.ToString();
    }

}
