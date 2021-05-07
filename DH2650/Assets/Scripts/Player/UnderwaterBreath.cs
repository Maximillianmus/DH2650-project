using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnderwaterBreath : MonoBehaviour
{

    //the time that a player can be without air
    public float MaxAirtime;
    public float DamageMultiplier = 1;
    public float AirUsageMultiplier = 1;
    public float AirRecoveryMultiplier = 1;
    //true if we are currently underwater
    public bool underwater;
    public float currentAir;

    public BreathingBar bar;

    public Image breathingBarImage;
    private PlayerHealth pHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentAir = MaxAirtime;
        bar.SetMaxBreathing(MaxAirtime);
        pHealth = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (underwater)
        {
            
            if(currentAir <= 0)
            {
                pHealth.TakeDamage(Time.deltaTime * DamageMultiplier);
            }
            else
            {
                currentAir -= Time.deltaTime * AirUsageMultiplier;
            }
            breathingBarImage.enabled = true;
        }
        else
        {
            if(currentAir >= MaxAirtime)
            {
                currentAir = MaxAirtime;
                breathingBarImage.enabled = false;
            }
            else
            {
                currentAir += Time.deltaTime * AirRecoveryMultiplier;
            }
          
            
        }

        bar.SetBreathing(currentAir);
    }


    public void AddBreath(float additionalBreath)
    {
        currentAir += additionalBreath;
        if (currentAir > MaxAirtime)
            currentAir = MaxAirtime;
    }
}
