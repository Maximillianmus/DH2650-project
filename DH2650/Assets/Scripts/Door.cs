using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Activation
{
    private bool open = false;
    [SerializeField] Vector3 change = new Vector3(0, 10, 0);

    public override void Activate()
    {
        // Close the door
        if(open)
        {
            transform.position -= change;
        }
        // Open the door
        else
        {

            transform.position += change;
        }
        open = !open;
    }
}
