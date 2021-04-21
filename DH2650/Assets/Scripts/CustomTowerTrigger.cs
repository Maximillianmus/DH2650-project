using UnityEngine;
using System.Collections;

public class CustomTowerTrigger : MonoBehaviour {

	public CustomTower twr;    
    public bool lockE;
	public GameObject curTarget;
    


    void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player") && !lockE)
		{   
			twr.target = other.gameObject.transform;            
            curTarget = other.gameObject;
			lockE = true;
		}
       
    }
	void Update()
	{
        if (curTarget)
        {
            //if (curTarget.CompareTag("Dead")) // get it from EnemyHealth
            //{
            //    lockE = false;
            //    twr.target = null;               
            //}
        }




        if (!curTarget) 
		{
			lockE = false;            
        }
	}
	void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Player") && other.gameObject == curTarget)
		{
			lockE = false;
            twr.target = null;            
        }
	}
	
}
