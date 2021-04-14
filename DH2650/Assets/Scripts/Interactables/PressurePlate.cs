using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] Activation[] connectedObjects = new Activation[0];
    [Header("Options")]
    [SerializeField] float delayOnEnter = 0f;
    [SerializeField] float delayOnExit = 0f;
    [SerializeField] bool triggerOnPlayer = false;
    [SerializeField] bool triggerOnItem = false;
    [SerializeField] bool triggerOnAnything = false;
    [SerializeField] bool triggerOnSpecificItem = false;
    [SerializeField] GameObject[] specificItems = new GameObject[0];

    private void OnTriggerEnter(Collider other)
    {
        if(triggerOnAnything)
        {
            StartCoroutine(Activate());
        }
        if(triggerOnPlayer)
        {
            if(other.tag == "Player")
            {
                StartCoroutine(Activate());
            }
        }
        if(triggerOnItem)
        {
            if(other.tag == "Item")
            {
                StartCoroutine(Activate());
            }
        }
        if(triggerOnSpecificItem)
        {
            for(int i = 0; i < specificItems.Length; ++i)
            {
                if(other.gameObject == specificItems[i])
                {
                    StartCoroutine(Activate());
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(triggerOnAnything)
        {
            StartCoroutine(DeActivate());
        }
        if(triggerOnPlayer)
        {
            if(other.tag == "Player")
            {
                StartCoroutine(DeActivate());
            }
        }
        if(triggerOnItem)
        {
           if(other.tag == "Item")
           {
                StartCoroutine(DeActivate());
           }
        }
        if(triggerOnSpecificItem)
        {
            for(int i = 0; i < specificItems.Length; ++i)
            {
                if(other.gameObject == specificItems[i])
                {
                    StartCoroutine(DeActivate());
                }
            }
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
