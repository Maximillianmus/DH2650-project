using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathingBar : MonoBehaviour
{

    public Slider slider;

    public void SetBreathing(float health)
    {
        slider.value = health;

    }

    public void SetMaxBreathing(float health)
    {
        slider.maxValue = health;
        slider.value = health;

    }

    public float getBreathingTime()
    {
        return slider.maxValue;
    }
}
