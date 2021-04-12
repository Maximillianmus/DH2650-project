using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol_Ground : MonoBehaviour
{
    // Start is called before the first frame update


    GameObject Player;

    public Transform[] waypoints;
    public float speed;
    public float detectionRange;

    private int waypointIndex;
    private float dist;
    private int playerMask;

    //Initialize here, or it won't work
    void Start()
    {
        Player = GameObject.Find("Player");

        detectionRange = 10f;
        waypointIndex = 0;
        playerMask = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {

        /*
         * The enemy will now chase the player if detected. If not, it goes back to patrolling.
         */

        dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        print(DetectPlayer());
        if(DetectPlayer())
        {
            ChasePlayer();
        } else
        {
            Patrol(dist);
        }

        
        
    }


    /*
     *  Enemy movement
     */
    void Patrol(float dist)
    {
        transform.LookAt(waypoints[waypointIndex].position);
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
            transform.LookAt(waypoints[waypointIndex].position); 
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].position, speed);
    }

    /*
     * Detects the player at "detectionRange"
     */
    bool DetectPlayer()
    {
        Ray forwardray = new Ray(transform.position, transform.forward);

        //Debug.DrawRay(transform.position, transform.forward * detectionRange, Color.red);

        RaycastHit playerHit;

        if(Physics.Raycast(forwardray, out playerHit, detectionRange, playerMask))
        {

            if (playerHit.collider.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }
    
    /*
     * Chases player
     */
    void ChasePlayer()
    {

        transform.LookAt(Player.transform);
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed);
    }
}
