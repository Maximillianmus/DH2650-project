using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] Activation[] connectedObjects = new Activation[0];
    [Header("Options")]
    [SerializeField] bool deActivateObjectOnExit = true;
    [SerializeField] float delayOnEnter = 0f;
    [SerializeField] float delayOnExit = 0f;
    [SerializeField] bool triggerOnPlayer = false;
    [SerializeField] bool triggerOnItem = false;
    [SerializeField] bool triggerOnAnything = false;
    [SerializeField] bool triggerOnSpecificItem = false;
    [SerializeField] GameObject[] specificItems = new GameObject[0];

    public OffHand offhand;
    private Collider recentCollider = null;
    private Collider c;

    public void Start()
    {
        offhand = GameObject.Find("OffHand").GetComponent<OffHand>();
        c = GetComponent<Collider>();
    }

    // To work around that objects lose physics and collision when picked up
    public void Update()
    {
        
        if(recentCollider != null)
        {
            Vector3 p = c.ClosestPoint(recentCollider.transform.position);
            // If the distance to the most recent object is to great OR if the object has been picked up, then call OnTriggerExit 
            if(Vector3.Distance(p, recentCollider.transform.position) > 1 || offhand.heldItem == recentCollider.gameObject)
            {
                OnTriggerExit(recentCollider);
                recentCollider = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(triggerOnAnything)
        {
            StartCoroutine(Activate());
        }
        if(triggerOnPlayer)
        {
            if(other.gameObject.name == "Foot")
            {
                StartCoroutine(Activate());
                return; // To avoid settingg recentCollider to player
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
        recentCollider = other;
    }

    private void OnTriggerExit(Collider other)
    {
        if(deActivateObjectOnExit)
        {
            if (triggerOnAnything)
            {
                StartCoroutine(DeActivate());
            }
            if (triggerOnPlayer)
            {
                if (other.tag == "Player")
                {
                    StartCoroutine(DeActivate());
                }
            }
            if (triggerOnItem)
            {
                if (other.tag == "Item")
                {
                    StartCoroutine(DeActivate());
                }
            }
            if (triggerOnSpecificItem)
            {
                for (int i = 0; i < specificItems.Length; ++i)
                {
                    if (other.gameObject == specificItems[i])
                    {
                        StartCoroutine(DeActivate());
                    }
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
