using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoatController : MonoBehaviour
{
    public Camera cam;
    public LevelPopUp levelPopUp;
    public NavMeshAgent agent;

    // Update is called once per frame
    void Update()
    {
        if (levelPopUp.IsActive()) return;
        if (Input.GetMouseButtonDown((0)))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
    