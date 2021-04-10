using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol_Ground : MonoBehaviour
{
    // Start is called before the first frame update


    GameObject Player;

    public Transform[] waypoints;
    public int speed;
    public float detectionRange;

    private int waypointIndex;
    private float dist;
    private int playerMask;

    //Initialize here, or it won't work
    void Start()
    {
        Player = GameObject.Find("Player Container");

        detectionRange = 10f;
        waypointIndex = 0;
        transform.LookAt(waypoints[waypointIndex].position);
        playerMask = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        //while(DetectPlayer())
        //{
        //    Vector3 playerPos = Player.transform.position;
        //    Vector3 direction = playerPos - this.transform.position;
        //    ChasePlayer(playerPos, direction);
        //}
        if(dist < 1f)
        {
            IncreaseIndex(); //Updates the waypoint when close enough
        }
        Patrol();


    }


    /*
     *  Enemy movement
     */
    void Patrol()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    /*
     *  Checks which way to patrol/move
     */
    void IncreaseIndex()
    {
        waypointIndex++;
        if(waypointIndex > waypoints.Length - 1)
        {
            waypointIndex = 0;
        }
        transform.LookAt(waypoints[waypointIndex].position);
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
    void ChasePlayer(Vector3 playerPos, Vector3 direction)
    {
        
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);

        this.transform.LookAt(playerPos);
        direction.y = 0;
        print(direction);
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
