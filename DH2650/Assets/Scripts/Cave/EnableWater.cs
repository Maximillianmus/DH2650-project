using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWater : MonoBehaviour
{

    public GameObject water;
    public GameObject waterContainer;

    private void OnTriggerEnter(Collider other)
    {
        print("Enable!");
        water.GetComponent<waves>().enabled = true;
        water.GetComponent<WaterEffects>().enabled = true;
        water.GetComponent<MeshRenderer>().enabled = true;

        waterContainer.GetComponent<waterEffectController>().enabled = true;
        waterContainer.GetComponent<BoxCollider>().enabled = true;
    }

}
