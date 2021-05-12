using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathingBar : MonoBehaviour
{

    public Image slider;
    private float maxBreathingTime;

    public void SetBreathing(float breath)
    {
        float amount = breath / maxBreathingTime;
        slider.fillAmount = amount;
    }

    public void SetMaxBreathing(float breath)
    {
        slider.fillAmount = 1;
        maxBreathingTime = breath;

    }

    public float getBreathingTime()
    {
        return maxBreathingTime;
    }
}
