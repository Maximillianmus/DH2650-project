using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleActivation : Activation
{

    public Container[] triggerObjects;
    public Vector3 changeOnActivation;

    //To keep track of activated items
    private int total;
    private int start = 0;
    private bool[] locked;
    private bool allTrue = false;
    private bool changed = false;
    // Start is called before the first frame update
    private void Start()
    {
        total = triggerObjects.Length;
        locked = new bool[triggerObjects.Length];
    }
    //Activated the items if conditions are satisfied.
    public override void Activate()
    {
        //Mirrors the locked-array for "activated items". = true means activated.
        for (int i = 0; i < triggerObjects.Length; i++)
        {
            if (triggerObjects[i].activated)
            {
                locked[i] = true;
            }
        }

        //If all items in locked-array is true, then entire things is true, therefore allTrue = True
        for (int i = 0; i < locked.Length; i++)
        {
            if (locked[i] == true)
            {
                start += 1;
            }
            if (start == total)
            {
                allTrue = true;
            }
        }

        //If the object hasnt changed and all items are true, modify the object.
        if (allTrue && !changed)
        {
            transform.position += changeOnActivation;
            changed = true;
        }

        //Resets all the "activated items".
        start = 0;
    }

    // Deactivated the items if the conditions are broken.
    public override void DeActivate()
    {
        //The opposite of the first forloop in "activate"
        for (int i = 0; i < triggerObjects.Length; i++)
        {
            if (!triggerObjects[i].activated)
            {
                locked[i] = false;
            }
        }

        //If any of the items are not activated, then alltrue = false;
        for (int i = 0; i < locked.Length; i++)
        {
            if (locked[i] == false)
            {
                start += 1;
            }
            if (start < total)
            {
                allTrue = false;
            }
        }

        //Change the object back, since the conditions of alltrue is not met.
        if (!allTrue && changed)
        {
            transform.position -= changeOnActivation;
            changed = false;
            start = 0;
        }
    }
}
