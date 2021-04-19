using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelTrigger : MonoBehaviour
{
    Material defaultMat;
    Material disappearMat;
    void OnTriggerEnter(Collider collider)
    {
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    void OnTriggerExit(Collider collider)
    {
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();

        meshRenderer.enabled = true;
    }

}
