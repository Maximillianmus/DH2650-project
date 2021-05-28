using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWater : MonoBehaviour
{
    public GameObject water;
    public GameObject waterContainer;

    private void OnTriggerStay(Collider other)
    {
        print("Disable!");
        water.GetComponent<waves>().enabled = false;
        water.GetComponent<WaterEffects>().enabled = false;
        water.GetComponent<MeshRenderer>().enabled = false;

        waterContainer.GetComponent<waterEffectController>().enabled = false;
        waterContainer.GetComponent<BoxCollider>().enabled = false;
    }

}
