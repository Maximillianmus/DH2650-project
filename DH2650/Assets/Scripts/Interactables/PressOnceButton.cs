using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressOnceButton : Interactable
{
    [SerializeField] Activation[] ObjectsToActivate = new Activation[0];
    [SerializeField] Activation[] ObjectsToDeactivate = new Activation[0];
    private bool pressed = false;

    public override void Interact(OffHand offHand)
    {
        PressButton();
    }

    public void PressButton()
    {
        if(!pressed)
        {
            // Make it look like button is pressed
            transform.localPosition -= new Vector3(0, 0.03f, 0);

            // Activate the connected object
            if (ObjectsToActivate.Length > 0)
            {
                foreach (Activation connObj in ObjectsToActivate)
                {
                    connObj.Activate();
                }
            }
            if (ObjectsToDeactivate.Length > 0)
            {
                foreach (Activation connObj in ObjectsToDeactivate)
                {
                    connObj.DeActivate();
                }
            }
        }
    }
}
