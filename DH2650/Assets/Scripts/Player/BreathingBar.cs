using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathingBar : MonoBehaviour
{

    public Image slider;
    private float maxBreathingTime;

    public void SetBreathing(float health)
    {
        //slider.value = health;
        float amount = health / maxBreathingTime;
        slider.fillAmount = amount;
    }

    public void SetMaxBreathing(float health)
    {
        //slider.maxValue = health;
        //slider.value = health;
        slider.fillAmount = 1;
        maxBreathingTime = health;

    }

    public float getBreathingTime()
    {
        //return slider.maxValue;
        return maxBreathingTime;
    }
}
