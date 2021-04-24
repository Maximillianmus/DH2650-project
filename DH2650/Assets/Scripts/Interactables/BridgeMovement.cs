using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeMovement : Activation
{
    // Start is called before the first frame update
    private bool haveMoved = false;
    [SerializeField] Vector3 changeOnActivation = new Vector3(0, 0, 0);

    //Moves the bridge upon activation
    public override void Activate()
    {
        if (!haveMoved)
        {
            transform.position += changeOnActivation;
            haveMoved = true; 
        }
    }

    //Returns to original state after deactivation
    public override void DeActivate()
    {
        if (haveMoved)
        {
            transform.position -= changeOnActivation;
            haveMoved = false;
        }

    }
}
