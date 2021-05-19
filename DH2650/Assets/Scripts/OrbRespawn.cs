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
        print("trigger!");
        print("Curr orb pos: " + orbPos);
        Collider collider = GameObject.Find("Cloudmesh").GetComponent<BoxCollider>();
        if(other == collider)
        {
            print("respawn!");
            Instantiate(orb, orbPos, Quaternion.identity);
            print("New orb pos: " + orbPos);
            Destroy(orb);
        }
        
    }
}
