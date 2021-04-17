using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Button : Interactable
{
    [SerializeField] Activation[] connectedObjects = new Activation[0];
    [SerializeField] float pressedForSeconds = 3f;
    private bool isPressed = false;

    public override void Interact(OffHand offHand)
    {
        PressButton();
    }

    /// Coroutine for deactivating things after some time
    private IEnumerator Pressed()
    {
        // Wait for some time before deactivating every connected object
        yield return new WaitForSeconds(pressedForSeconds);

        // Activate the connected object
        if(connectedObjects.Length > 0)
        {
            foreach (Activation connObj in connectedObjects)
            {
                connObj.DeActivate();
            }
        }
        transform.localPosition += new Vector3(0, 0.03f, 0);
        isPressed = false;
    }

    private void PressButton()
    {
        // If button is not pressed already, the player can press it
        if(!isPressed)
        {
            transform.localPosition -= new Vector3(0, 0.03f, 0);
            isPressed = true;
            // Activate the connected object
            if(connectedObjects.Length > 0)
            {
                foreach (Activation connObj in connectedObjects)
                {
                    connObj.Activate();
                }
            }
            StartCoroutine(Pressed());
        }
    }

}
