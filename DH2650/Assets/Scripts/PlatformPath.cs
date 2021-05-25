using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPath : MonoBehaviour
{

    [Header("Waypoints to move between")]
    public Transform[] waypoints;
    [Header("Move speed ~10 appropriate")]
    public float speed;

    private int waypointIndex;
    private float dist;
    private Vector3 startpos;

    void Start()
    {
        waypointIndex = 0;
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if (dist < 1f)
        {
            /*
                *  Checks which way to patrol/move
                */
            //Updates the waypoint when close enough
            waypointIndex++;
            if (waypointIndex > waypoints.Length - 1)
            {
                waypointIndex = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].position, speed * Time.deltaTime);

    }

}
