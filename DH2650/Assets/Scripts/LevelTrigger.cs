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
        meshRenderer.material = disappearMat;
    }

    void OnTriggerExit(Collider collider)
    {
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.material = defaultMat;
    }
    // Start is called before the first frame update
    void Start()
    {
        defaultMat = AssetDatabase.GetBuiltinExtraResource<Material>("Default-Material.mat");
        disappearMat = AssetDatabase.GetBuiltinExtraResource<Material>("Default-Terrain-Standard.mat");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
