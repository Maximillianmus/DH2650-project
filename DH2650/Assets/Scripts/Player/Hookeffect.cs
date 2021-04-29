using System.Collections;
using System.Collections.Generic;
//using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class Hookeffect : MonoBehaviour
{
    GameObject player;
    UnityEngine.Rendering.VolumeProfile volumeProfile;
    UnityEngine.Rendering.Universal.Vignette vignette;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        volumeProfile = GetComponent<UnityEngine.Rendering.Volume>()?.profile;
        if (!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));

        // You can leave this variable out of your function, so you can reuse it throughout your class.
        

        if (!volumeProfile.TryGet(out vignette)) throw new System.NullReferenceException(nameof(vignette));

        //vignette.intensity.Override(0.5f);

    }

    // Update is called once per frame
    void Update()
    {
        print(player.GetComponent<PlayerMovement>().moveSpeed);
        flyEffect();
    }

    /*
     * Flying feeling with vignette effect
     */
    void flyEffect()
    {
        vignette.intensity.value = (1 - (1f / player.GetComponent<PlayerMovement>().moveSpeed));
        //vignette.intensity.Override(1 / player.GetComponent<PlayerMovement>().moveSpeed);
    }

}
