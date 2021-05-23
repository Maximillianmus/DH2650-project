using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCaveLighning : MonoBehaviour
{

    public GameObject dirSkyLight;

    private void OnTriggerEnter(Collider other)
    {
        dirSkyLight.GetComponent<Light>().enabled = false;
    }


}
