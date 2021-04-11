using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleObelisk : ContainerInteraction
{
    [Header("Must Set these")]
    [SerializeField] Transform[] itemContainers = new Transform[2];
    [SerializeField] GameObject[] containedItems = new GameObject[2];
    [Header("Optional")]
    [SerializeField] Activation[] connectedObjects = new Activation[0];
    private int maxNumberOfItems = 2;
    private int currentNumberOfItems = 0;

    // Define what happens when player interacts with Obelisk
    public override void Interact(GameObject item, OffHandInteraction offHandInteraction)
    {   
        // If offHand has an item, then try and place that item
        if(offHandInteraction.slotFull)
        {
            PlaceItem(item, offHandInteraction);
        }
        // If offHand has no item, then try and take item
        else
        {
            TakeItem(offHandInteraction);
        }
    }

    private void PlaceItem(GameObject item, OffHandInteraction offHandInteraction)
    {
        // If container has no item already then, the player can place the item.
        if(currentNumberOfItems < maxNumberOfItems)
        {
            // offHand no longer has an item
            offHandInteraction.slotFull = false;
            offHandInteraction.heldItem = null;

            // Prevent player from selecting the item
            item.layer = 0;

            // Container now holds the item
            containedItems[currentNumberOfItems] = item;
            
            
            // Reset its position in the container
            item.transform.SetParent(itemContainers[currentNumberOfItems]);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.Euler(Vector3.zero);

            currentNumberOfItems++;

            // Make it so item has collision
            Collider coll = item.GetComponent<Collider>();
            coll.isTrigger = false;

            // Activate the connected objects
            if(currentNumberOfItems == maxNumberOfItems)
            {
                if(connectedObjects.Length > 0)
                {
                    foreach (Activation connObj in connectedObjects)
                    {
                        connObj.Activate();
                    }
                }
            }
        }
    }

    private void TakeItem(OffHandInteraction offHandInteraction)
    {
        // If container has an item, then the player can take it.
        if(currentNumberOfItems > 0)
        {
            GameObject item = containedItems[currentNumberOfItems-1];
            // Add item to off hand
            item.transform.SetParent(offHandInteraction.offHand.transform);
            // Reset position and rotation
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.Euler(Vector3.zero);

            // Make it possible to target item again
            item.layer = 7;

            // offHand has an item
            offHandInteraction.slotFull = true;
            offHandInteraction.heldItem = item;

            // Make it so item has no collision
            Collider coll = item.GetComponent<Collider>();
            coll.isTrigger = true;
            
            // Container no longer has an item
            item = null;
            currentNumberOfItems--;

            // DeActivate the connected objects
            foreach (Activation connObj in connectedObjects)
                {
                    connObj.DeActivate();
                }
        }
    }
    

}
