using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLightning : MonoBehaviour
{
    public GameObject dirSkyLight;

    private void OnTriggerEnter(Collider other)
    {
        dirSkyLight.GetComponent<Light>().enabled = true;
    }
}
