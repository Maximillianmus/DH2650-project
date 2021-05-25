using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnderwaterBreath : MonoBehaviour
{

    //the time that a player can be without air
    public float MaxAirtime;
    public float wateDrag = 0.3f;
    public float DamageMultiplier = 1;
    public float AirUsageMultiplier = 1;
    public float AirRecoveryMultiplier = 1;
    //true if we are currently underwater
    public bool underwater;
    public float currentAir;

    public BreathingBar bar;

    public Image breathingBarImage;
    public Image breathingBarBackground;
    private PlayerHealth pHealth;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        currentAir = MaxAirtime;
        bar.SetMaxBreathing(MaxAirtime);
        pHealth = FindObjectOfType<PlayerHealth>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused) return;
        if (underwater)
        {
            rb.drag = wateDrag;
            if(currentAir <= 0)
            {
                pHealth.TakeDamage(Time.deltaTime * DamageMultiplier);
            }
            else
            {
                currentAir -= Time.deltaTime * AirUsageMultiplier;
            }
            breathingBarImage.enabled = true;
            breathingBarBackground.enabled = true;
        }
        else
        {
            rb.drag = 0;
            if(currentAir >= MaxAirtime)
            {
                currentAir = MaxAirtime;
                breathingBarImage.enabled = false;
                breathingBarBackground.enabled = false;
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
