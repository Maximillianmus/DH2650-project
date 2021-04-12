using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable
{
    [SerializeField] Activation[] connectedObjects = new Activation[0];
    private bool isActive = false;

    public override void Interact(OffHand offHand)
    {
        pullLever();
    }

    private void pullLever()
    {
        isActive = !isActive;

        if(isActive)
        {
            // Activate the connected object
            if(connectedObjects.Length > 0)
            {
                foreach (Activation connObj in connectedObjects)
                {
                    connObj.Activate();
                }
            }
            transform.localPosition = new Vector3(0.4f, -0.1f, 0);
            transform.Rotate(0, 0, -60);
        }
        else
        {
            // Activate the connected object
            if(connectedObjects.Length > 0)
            {
                foreach (Activation connObj in connectedObjects)
                {
                    connObj.DeActivate();
                }
            }

            transform.localPosition = new Vector3(-0.2f, -0.1f, 0);
            transform.Rotate(0, 0, 60);
        }
    }


}
