using System.Collections;
using System.Collections.Generic;
//using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class Hookeffect : MonoBehaviour
{

    //Effects
    UnityEngine.Rendering.VolumeProfile volumeProfile;
    UnityEngine.Rendering.Universal.LensDistortion LD;

    //Key associated
    public GrapplingGun grapplingGun; //The Q button
    KeyCode playerPull;

    // Start is called before the first frame update
    void Start()
    {
        //NULL atm
        playerPull = grapplingGun.HookShootButton;

        volumeProfile = GetComponent<UnityEngine.Rendering.Volume>()?.profile;
        if (!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));

        if (!volumeProfile.TryGet(out LD)) throw new System.NullReferenceException(nameof(LD));


    }

    // Update is called once per frame
    void Update()
    {
        //print(player.GetComponent<PlayerMovement>().moveSpeed);
        flyEffect();
    }

    /*
     * Flying feeling with LensDistortion effect
     */
    void flyEffect()
    {


        if (Input.GetKey(playerPull) && grapplingGun.IsGrapplingWithJoint())
        {
            LD.intensity.value -= 0.05f;
            
            if(LD.intensity.value < -0.7f)
            {
                LD.intensity.value = -0.7f;
            }
        } else
        {
            if(LD.intensity.value < 0)
            {
                LD.intensity.value += 0.1f;
            } else if(LD.intensity.value > 0)
            {
                LD.intensity.value = 0;
            }
        }

    }

}
