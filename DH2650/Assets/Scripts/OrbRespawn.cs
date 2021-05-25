using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbRespawn : MonoBehaviour
{
    private GameObject orb;
    private Vector3 orbPos;

    // Start is called before the first frame update
    void Start()
    {
        orb = this.gameObject;
        orbPos = orb.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider collider = GameObject.Find("Cloudmesh").GetComponent<BoxCollider>();
        if(other == collider)
        {
            Instantiate(orb, orbPos, Quaternion.identity);
            Destroy(orb);
        }
        
    }
}
