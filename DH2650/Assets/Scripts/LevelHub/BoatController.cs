using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoatController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public LevelPopUp levelPopUp;
    private float distanceThreshold = 10;
    public LevelManager levelmanager;

    // Update is called once per frame
    void Update()
    {
        if (levelPopUp.IsActive()) return;

        if(Input.GetMouseButtonDown((0)))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        //if (agent.velocity.magnitude > 0 && agent.remainingDistance < distanceThreshold)
        //{
        //    LevelPoint closestLevel = levelmanager.GetClosestLevel(transform);
        //    if (closestLevel != null)
        //        levelPopUp.UpdateLevelInfo(closestLevel.SceneName);
        //}
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("colliding");
    }
}
    