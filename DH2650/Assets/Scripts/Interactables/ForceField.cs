using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : Activation
{
    public bool isOnByDefault = true;
    public int requiredActivations = 1;
    public int currentNumActivations = 0;

    public MeshRenderer meshRenderer;
    public Collider coll;

    public void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        coll = GetComponent<Collider>();
    }

    public override void Activate()
    {
        currentNumActivations++;
        if(currentNumActivations >= requiredActivations)
        {
            if(isOnByDefault)
            {
                meshRenderer.enabled = false;
                coll.enabled = false;
            } 
            else
            {
                meshRenderer.enabled = true;
                coll.enabled = true;
            }
        }
    }

    public override void DeActivate()
    {
        currentNumActivations--;
        if(currentNumActivations < requiredActivations)
        {
            if(isOnByDefault)
            {
                meshRenderer.enabled = true;
                coll.enabled = true;
            }
            else
            {
                meshRenderer.enabled = false;
                coll.enabled = false;
            }

        }
    }

}
