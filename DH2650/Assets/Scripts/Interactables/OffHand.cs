using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffHand : MonoBehaviour
{
    [SerializeField] Transform m_camera;
    [SerializeField] float pickUpRange = 2f;
    [SerializeField] LayerMask whatIsInteractable;
    public LayerMask GroundLayer;
    [SerializeField] KeyCode InteractButton;
    public bool slotFull;
    public GameObject heldItem;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit; 
        bool performedAction = false;

        // Determine what player is looking at
        if (Physics.Raycast(m_camera.position, m_camera.forward, out hit, pickUpRange, whatIsInteractable)){

            // I have seperated these in case we might want to do different things based on what player is interacting with

            if(hit.collider.tag == "Item" && Input.GetKeyDown(KeyCode.E))
            {
                hit.collider.gameObject.GetComponent<Interactable>().Interact(this);
                performedAction = true;
            }
            
            else if(hit.collider.tag == "Container" && Input.GetKeyDown(InteractButton))
            {
                hit.collider.gameObject.GetComponent<Interactable>().Interact(this);
                performedAction = true;
            }

            else if(hit.collider.tag == "Interactable" && Input.GetKeyDown(InteractButton))
            {
                hit.collider.gameObject.GetComponent<Interactable>().Interact(this);
                performedAction = true;
            }
        }
        // If we have not done any other action, hold an item and press E. Then we drop it.
        if(!performedAction && slotFull && Input.GetKeyDown(KeyCode.E))
        {
            DropItem();
        }
    }

    private void DropItem()
    {
        slotFull = false;
        heldItem.transform.SetParent(null);

        // Restore collisions and physics and stuff
        Rigidbody rb = heldItem.GetComponent<Collider>().gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;

        // THIS DOES NOT WORK
        //heldItem.layer = GroundLayer.value;

        Collider coll = heldItem.GetComponent<Collider>().gameObject.GetComponent<Collider>();
        coll.isTrigger = false;

        heldItem = null;
    }

}
