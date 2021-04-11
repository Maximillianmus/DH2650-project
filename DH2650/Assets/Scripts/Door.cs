using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Activation
{
    private bool isOpen = false;
    [SerializeField] Vector3 changeOnActivation = new Vector3(0, 10, 0);

    // Opens the door
    public override void Activate()
    {
        if(!isOpen)
        {
            transform.position += changeOnActivation;
            isOpen = true;
        }
            
    }

    // Closes the door
    public override void DeActivate()
    {
        if(isOpen)
        {
            transform.position -= changeOnActivation;
            isOpen = false;
        }
        
    }
}
