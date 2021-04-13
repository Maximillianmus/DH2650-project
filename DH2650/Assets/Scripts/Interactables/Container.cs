using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : Interactable
{
    public int currentNumberOfItems = 0;
    [SerializeField] int maxNumberOfItems = 0;

    [SerializeField] Transform[] itemContainers = new Transform[0];
    public Activation[] connectedObjects = new Activation[0];

    public GameObject[] containedItems;
    public bool[] usedContainers;

    void Start()
    {
        GameObject[] containedItems = new GameObject[maxNumberOfItems];
        bool[] usedContainers = new bool[maxNumberOfItems];
    }

    // Define what happens when player interacts with the Container
    public override void Interact(OffHand offHand)
    {   
        // If offHand has an item, 
        if(offHand.slotFull)
        {
            if(currentNumberOfItems < maxNumberOfItems)
            {
                PlaceItem(offHand.heldItem, offHand);
            }
        }
        // If offHand has no item, then try and take an item
        else
        {
            if(currentNumberOfItems > 0)
            {
                GameObject item = GetItemFromContainer();
                TakeItem(item, offHand);
            }
        }
    }

    private void PlaceItem(GameObject item, OffHand offHand)
    {
        currentNumberOfItems++;

        // offHand no longer has an item
        offHand.slotFull = false;
        offHand.heldItem = null;

        // Item is now in a container
        item.GetComponent<Item>().inContainer = true;
        item.GetComponent<Item>().container = this;

        // Make it possible for the player to take the item
        item.layer = 6;

        // Get index of where to place item
        int pos = GetIndexOfEmptyContainer();

        // Container now holds the item
        containedItems[pos] = item;
        usedContainers[pos] = true;
        item.transform.SetParent(itemContainers[pos]);

        // Reset its position in the container
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.Euler(Vector3.zero);

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

    // Take item from container
    public void TakeItem(GameObject item, OffHand offHand)
    {
        int pos = getIndexOfItem(item);

        if(!(pos == -1))
        {
            // Move item to offhand
            item.transform.SetParent(offHand.transform);
            // Reset position and rotation
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.Euler(Vector3.zero);

            // Item is now on the player layer
            item.layer = 7;

            // offHand has an item
            offHand.slotFull = true;
            offHand.heldItem = item;

            // Make it so item has no collision
            Collider coll = item.GetComponent<Collider>();
            coll.isTrigger = true;
            
            // Container no longer has an item
            containedItems[pos] = null;
            usedContainers[pos] = false;
            currentNumberOfItems--;

            if(connectedObjects.Length > 0)
            {
                foreach (Activation connObj in connectedObjects)
                {
                    connObj.DeActivate();
                }
            }
        }
    }

    private int GetIndexOfEmptyContainer()
    {
        for(int i = 0; i < usedContainers.Length; ++i)
        {
            // If slot is not used, then return that index.
            if(!usedContainers[i])
            {
                return i;
            }
        }
        print("No Space in container");
        return -1;
    }

    private GameObject GetItemFromContainer()
    {
        for(int i = 0; i < usedContainers.Length; ++i)
        {
            // If slot has an item, return it
            if(usedContainers[i])
            {
                return containedItems[i];
            }
        }
        print("There is no item");
        return null;
    }

    public int getIndexOfItem(GameObject item)
    {
        for(int i = 0; i < containedItems.Length; ++i)
        {
            if(item == containedItems[i])
            {
                return i;
            }
        }
        return -1;
    }

}
