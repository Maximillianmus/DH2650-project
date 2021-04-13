using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : Interactable
{
    // Used to make sure the object doesn't change anything about the player movement.
    public int PlayerLayer;
    // Define what happens when player interacts with the orb
    public override void Interact(OffHand offHand)
    {
        PickUp(offHand);
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
            Rigidbody rb = GetComponent<Collider>().gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;


            gameObject.layer = PlayerLayer;
            
            Collider coll = GetComponent<Collider>().gameObject.GetComponent<Collider>();
            coll.isTrigger = true;
        }
    }

}
