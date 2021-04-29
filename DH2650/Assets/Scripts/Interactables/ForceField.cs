using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : Activation
{
    public int requiredActivations = 1;
    public int currentNumberActivation = 0;
    public MeshRenderer meshRenderer;
    public Collider coll; 

    public void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        coll = GetComponent<Collider>();
    }

    public override void Activate()
    {
        currentNumberActivation++;
        if(currentNumberActivation == requiredActivations)
        {
            meshRenderer.enabled = false;
            coll.enabled = false;
        }
    }

    public override void DeActivate()
    {
        currentNumberActivation--;
        meshRenderer.enabled = true;
        coll.enabled = true;
    }
}
