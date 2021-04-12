using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] Activation[] connectedObjects = new Activation[0];
    [Header("Options")]
    [SerializeField] float delayOnEnter = 0f;
    [SerializeField] float delayOnExit = 0f;
    [SerializeField] bool triggerOnlyOnPlayer = false;

    private void OnTriggerEnter(Collider other)
    {
        if(triggerOnlyOnPlayer)
        {
            if(other.tag == "Player")
            {
                StartCoroutine(Activate());
            }
        }
        else
        {
            StartCoroutine(Activate());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(triggerOnlyOnPlayer)
        {
            if(other.tag == "Player")
            {
                StartCoroutine(DeActivate());
            }
        }
        else
        {
            StartCoroutine(DeActivate());
        }
    }

    private IEnumerator Activate()
    {
        // Wait for some time before deactivating every connected object
        yield return new WaitForSeconds(delayOnEnter);

        // Activate the connected object
        if(connectedObjects.Length > 0)
        {
            foreach (Activation connObj in connectedObjects)
            {
                connObj.Activate();
            }
        }
    }

    private IEnumerator DeActivate()
    {
        // Wait for some time before deactivating every connected object
        yield return new WaitForSeconds(delayOnExit);

        // Activate the connected object
        if(connectedObjects.Length > 0)
        {
            foreach (Activation connObj in connectedObjects)
            {
                connObj.DeActivate();
            }
        }
    }
}
