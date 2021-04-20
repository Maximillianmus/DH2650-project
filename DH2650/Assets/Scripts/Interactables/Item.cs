using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    // Used to make sure the object doesn't change anything about the player movement.
    [SerializeField] int PlayerLayer = 7;
    public bool inContainer = false;
    public Container container;

    private Collider coll;
    private Rigidbody rb;

    void Start()
    {
        coll = this.GetComponent<Collider>();
        rb = this.GetComponent<Rigidbody>();
    }


    // Define what happens when player interacts with item
    public override void Interact(OffHand offHand)
    {
        // If it is in a container, let that handle how the interaction works
        if(inContainer)
        {
            container.TakeItem(gameObject, offHand);
        }
        else
        {
            PickUp(offHand);
        }
    }

    private void PickUp(OffHand offHand)
    {
        // If player has no item in offHand then take the item.
        if(!offHand.slotFull)
        {
            offHand.slotFull = true;
            offHand.heldItem = this.gameObject;
            
            // Move the item to the offhand
            transform.SetParent(offHand.transform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            

            // Remove collisions and physics and stuff
            rb.isKinematic = true;
            gameObject.layer = PlayerLayer;
            coll.isTrigger = true;
            
            // Remove smoother movement for item
            rb.interpolation = RigidbodyInterpolation.None;
        }
    }
    public void ReleaseFromContainer()
    {
        if(inContainer)
        {
            transform.SetParent(null);
            gameObject.layer = 6;

            // Return collisions and physics
            coll.isTrigger = false;
            rb.isKinematic = false;

            // update things in container
            int pos = container.getIndexOfItem(gameObject);

            container.containedItems[pos] = null;
            container.usedContainers[pos] = false;
            container.currentNumberOfItems--;

            container.DeActivateObjects();

            inContainer = false;
            container = null;

            
        }
  
    }
}
