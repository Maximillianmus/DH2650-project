using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : ItemInteraction
{
    // Used to make sure the object doesn't change anything about the player movement.
    public LayerMask PlayerLayer;
    // Define what happens when player interacts with the orb
    public override void Interact(OffHandInteraction offHandInteraction)
    {
        PickUp(offHandInteraction);
    }

    private void PickUp(OffHandInteraction offHandInteraction)
    {
        // If player has no item in offHand then take the item.
        if(!offHandInteraction.slotFull)
        {
            offHandInteraction.slotFull = true;
            offHandInteraction.heldItem = this.gameObject;

            // Move the item to the offhand
            transform.SetParent(offHandInteraction.offHand.transform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);

            // Remove collisions and physics and stuff
            Rigidbody rb = GetComponent<Collider>().gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            gameObject.layer = PlayerLayer;
            Collider coll = GetComponent<Collider>().gameObject.GetComponent<Collider>();
            coll.isTrigger = true;
        }
    }

}
