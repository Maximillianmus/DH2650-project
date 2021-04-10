using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleObelisk : ContainerInteraction
{
    [SerializeField] Transform[] itemContainers = new Transform[2];
    [SerializeField] GameObject[] containedItems = new GameObject[2];
    [SerializeField] Activation connectedObject;
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

            // Activate the connected object
            if(currentNumberOfItems == maxNumberOfItems)
                connectedObject.Activate();
        }
    }

    private void TakeItem(OffHandInteraction offHandInteraction)
    {
        // If container has an item, then the player can take it.
        if(currentNumberOfItems > 0)
        {
            // Add item to off hand
            containedItems[currentNumberOfItems-1].transform.SetParent(offHandInteraction.offHand.transform);
            // Reset position and rotation
            containedItems[currentNumberOfItems-1].transform.localPosition = Vector3.zero;
            containedItems[currentNumberOfItems-1].transform.localRotation = Quaternion.Euler(Vector3.zero);

            // offHand has an item
            offHandInteraction.slotFull = true;
            offHandInteraction.heldItem = containedItems[currentNumberOfItems-1];

            // Make it so item has no collision
            Collider coll = containedItems[currentNumberOfItems-1].GetComponent<Collider>();
            coll.isTrigger = true;
            
            // Container no longer has an item
            containedItems[currentNumberOfItems-1] = null;
            currentNumberOfItems--;

            // Activate the connected object
            if(currentNumberOfItems == 1)
                connectedObject.Activate();
        }
    }
    

}
