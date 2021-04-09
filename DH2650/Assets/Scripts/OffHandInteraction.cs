using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffHandInteraction : MonoBehaviour
{
    public Transform camera, offHand;
    public float pickUpRange = 2f;
    public LayerMask whatIsInteractable;
    public bool slotFull;
    public GameObject heldItem;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit; 
        if (Physics.Raycast(camera.position, camera.forward, out hit, pickUpRange, whatIsInteractable)){
            // If we look at an Item, press E, and dont hold anything. Pick it up.
            if(hit.collider.tag == "Item" && Input.GetKeyDown(KeyCode.E) && !slotFull)
            {
                print("Picking up " + hit.collider.name);
                PickUp(hit);
            }
            // If we look at a container, press E, and hold something. Place it down.
            if(hit.collider.tag == "Container" && Input.GetKeyDown(KeyCode.E) && slotFull)
            {
                print("Placing item " + hit.collider.name);
                Place(hit);
            }
        }


    }

    private void PickUp(RaycastHit item)
    {
        slotFull = true;
        heldItem = item.collider.gameObject;

        // Move the item to the offhand
        item.transform.SetParent(offHand.transform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.Euler(Vector3.zero);

        // Remove collisions and physics and stuff
        Rigidbody rb = item.collider.gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        Collider coll = item.collider.gameObject.GetComponent<Collider>();
        coll.isTrigger = true;

    }

    private void Place(RaycastHit container)
    {
        slotFull = false;
        
        // Place item in container
        heldItem.transform.SetParent(container.transform.Find("ItemContainer"));
        // Reset its position in new container
        heldItem.transform.localPosition = Vector3.zero;
        heldItem.transform.localRotation = Quaternion.Euler(Vector3.zero);

        // Make it so item has collision
        Collider coll = heldItem.GetComponent<Collider>();
        coll.isTrigger = false;

        heldItem = null;
    }
    
}
