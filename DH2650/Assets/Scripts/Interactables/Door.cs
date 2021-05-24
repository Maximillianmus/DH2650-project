using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Activation
{
    private bool isOpen = false;
    public bool onlyActivate = false;
    public bool onlyDeactivate = false;
    [SerializeField] Vector3 changeOnActivation = new Vector3(0, 10, 0);

    // Opens the door
    public override void Activate()
    {
        if(!isOpen && !onlyDeactivate)
        {
            transform.position += changeOnActivation;
            isOpen = true;
        }
            
    }

    // Closes the door
    public override void DeActivate()
    {
        if(isOpen && !onlyActivate)
        {
            transform.position -= changeOnActivation;
            isOpen = false;
        }
        
    }
}
