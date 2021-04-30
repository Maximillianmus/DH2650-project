using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterEffectController : MonoBehaviour
{
    private Transform water;
    private WaterEffects waterEff;
    private void Start()
    {
        if (enabled)
        {
            water = transform.GetChild(0);

            waterEff = water.transform.GetComponent<WaterEffects>();
        }

        
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Head")
        {
            waterEff.enabled = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Head")
        {
            waterEff.enabled = false;
        }
    }


}
