using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffHandInteraction : MonoBehaviour
{
    
    [SerializeField] Transform m_camera;
    public Transform offHand;
    [SerializeField] float pickUpRange = 2f;
    [SerializeField] LayerMask whatIsInteractable;
    public int GroundLayer;
    public bool slotFull;
    public GameObject heldItem;
    public KeyCode InteractButton;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit; 
        bool performedAction = false;

        // Determine what player is looking at
        if (Physics.Raycast(m_camera.position, m_camera.forward, out hit, pickUpRange, whatIsInteractable)){

            // If we look at an Item, press E, and dont hold anything. Pick it up.
            if(hit.collider.tag == "Item" && Input.GetKeyDown(KeyCode.E))
            {
                hit.collider.gameObject.GetComponent<ItemInteraction>().Interact(this);
                performedAction = true;
            }
            // If we look at a container, press E, then we interact with it.
            else if(hit.collider.tag == "Container" && Input.GetKeyDown(InteractButton))
            {
                hit.collider.gameObject.GetComponent<ContainerInteraction>().Interact(heldItem, this);
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
        heldItem.layer = GroundLayer;
        Collider coll = heldItem.GetComponent<Collider>().gameObject.GetComponent<Collider>();
        coll.isTrigger = false;

        heldItem = null;
    }

}
