using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : Interactable
{
    public int currentNumberOfItems = 0;
    [Header("Must Set these")]
    [SerializeField] int maxNumberOfItems = 0;
    [SerializeField] Transform[] itemContainers = new Transform[0];
    public GameObject[] containedItems = new GameObject[0];
    public bool[] usedContainers = new bool[0];

    [Header("Options for connected Objects")]
    public Activation[] connectedObjects = new Activation[0];
    [SerializeField] bool triggerWhenFull = false;
    [SerializeField] bool triggerOnSpecificItem = false;
    [SerializeField] GameObject[] specificItems = new GameObject[0];


    // Make it easier to make it so container already has items
    void Start()
    {
        foreach (GameObject item in containedItems)
        {
            if(item != null)
            {
                item.GetComponent<Item>().inContainer = true;
                item.GetComponent<Item>().container = this;
                item.layer = 6;
                
                // Reset its position in the container
                item.transform.localPosition = Vector3.zero;
                item.transform.localRotation = Quaternion.Euler(Vector3.zero);

                item.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;

                // Make it so item has collision
                Collider coll = item.GetComponent<Collider>();
                coll.isTrigger = false;
                Rigidbody rb = item.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                
            }
        }
        ActivateObjects();
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

        item.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
        
        ActivateObjects();
    }

    // Take item from container
    public void TakeItem(GameObject item, OffHand offHand)
    {
        // If you dont have anything in the offhand, you can take the item
        if(!offHand.slotFull)
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
                
                item.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;

                item.GetComponent<Item>().inContainer = false;
                item.GetComponent<Item>().container = null;
                
                // Container no longer has an item
                containedItems[pos] = null;
                usedContainers[pos] = false;
                currentNumberOfItems--;

                DeActivateObjects();
            }
        }
        // Else just release it from the container
        else
        {
            item.GetComponent<Item>().ReleaseFromContainer();
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

    private void ActivateObjects()
    {
        bool activate = false;

        if(triggerWhenFull && triggerOnSpecificItem)
        {
            // Container must be full
            if(currentNumberOfItems == maxNumberOfItems)
            {
                // Check that all specified items are in the container
                int numCorrect = 0;
                foreach (GameObject obj in containedItems)
                {
                    for(int i = 0; i <specificItems.Length; ++i)
                    {
                        if(obj == specificItems[i])
                        {
                            numCorrect++;
                            break;
                        }
                    }
                }
                if(numCorrect == specificItems.Length)
                {
                    activate = true;
                }
            }
        }

        if(triggerWhenFull)
        {
            if(currentNumberOfItems == maxNumberOfItems)
            {
                activate = true;
            }
        }

        if(triggerOnSpecificItem)
        {
            // Check that all specified items are in the container
            int numCorrect = 0;
            foreach (GameObject obj in containedItems)
            {
                for(int i = 0; i <specificItems.Length; ++i)
                {
                    if(obj == specificItems[i])
                    {
                        numCorrect++;
                        break;
                    }
                }
            }
            if(numCorrect == specificItems.Length)
            {
                activate = true;
            }
        }

        if(activate)
        {
            foreach (Activation connObj in connectedObjects)
            {
                if(connObj !=null)
                {
                    connObj.Activate();
                }
            }
        }
    }

    public void DeActivateObjects()
    {
        bool deactivate = false;

        if(triggerWhenFull)
        {
            if(currentNumberOfItems != maxNumberOfItems)
            {
                deactivate = true;
            }
        }

        if(triggerOnSpecificItem)
        {
            // Check if all 
            int numCorrect = 0;
            foreach (GameObject obj in containedItems)
            {
                for(int i = 0; i <specificItems.Length; ++i)
                {
                    if(obj == specificItems[i])
                    {
                        numCorrect++;
                        break;
                    }
                }
            }
            if(numCorrect != specificItems.Length)
            {
                deactivate = true;
            }
        }

        if(deactivate)
        {
            foreach (Activation connObj in connectedObjects)
            {
                if(connObj != null)
                {
                    connObj.DeActivate();
                }
            }
        }

    }

}
