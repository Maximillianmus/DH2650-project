using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaRunForceField : Activation
{
    public int requiredActivations = 1;
    public int currentNumberActivation = 0;
    public MeshRenderer meshRenderer;
    public Collider coll;

    public Lava connectedLava;
    public float stopedForSeconds = 0;
    public float lavaSpeed = 1;

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

            connectedLava.isMoving = false;
            connectedLava.travelSpeed = lavaSpeed;
            StartCoroutine(ResumeLava());
        }
    }

    public override void DeActivate()
    {
        currentNumberActivation--;
        meshRenderer.enabled = true;
        coll.enabled = true;
    }

    private IEnumerator ResumeLava()
    {
        yield return new WaitForSeconds(stopedForSeconds);
        connectedLava.isMoving = true;
    }

}
