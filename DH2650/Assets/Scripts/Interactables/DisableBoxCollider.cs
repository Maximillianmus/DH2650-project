using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBoxCollider : MonoBehaviour
{
    public BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }


    private void OnTriggerExit(Collider other)
    {
        boxCollider.enabled = false;
    }

}
