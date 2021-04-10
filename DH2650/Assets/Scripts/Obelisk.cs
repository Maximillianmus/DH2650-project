using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obelisk : ContainerInteraction
{
    
    [SerializeField] Transform itemContainer;
    [SerializeField] GameObject containedItem;
    [SerializeField] Activation connectedObject;
    private bool hasItem = false;

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
        if(!hasItem)
        {
            // offHand no longer has an item
            offHandInteraction.slotFull = false;
            offHandInteraction.heldItem = null;

            // Container now holds the item
            containedItem = item;
            hasItem = true;
            
            // Reset its position in the container
            item.transform.SetParent(itemContainer);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.Euler(Vector3.zero);

            // Make it so item has collision
            Collider coll = item.GetComponent<Collider>();
            coll.isTrigger = false;

            // Activate the connected object
            connectedObject.Activate();
        }
    }

    private void TakeItem(OffHandInteraction offHandInteraction)
    {
        // If container has an item, then the player can take it.
        if(hasItem)
        {
            // Add item to off hand
            containedItem.transform.SetParent(offHandInteraction.offHand.transform);
            // Reset position and rotation
            containedItem.transform.localPosition = Vector3.zero;
            containedItem.transform.localRotation = Quaternion.Euler(Vector3.zero);

            // offHand has an item
            offHandInteraction.slotFull = true;
            offHandInteraction.heldItem = containedItem;

            // Make it so item has no collision
            Collider coll = containedItem.GetComponent<Collider>();
            coll.isTrigger = true;
            
            // Container no longer has an item
            hasItem = false;
            containedItem = null;

            // Activate the connected object
            connectedObject.Activate();
        }
    }
}