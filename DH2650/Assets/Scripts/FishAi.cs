using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAi : MonoBehaviour
{
    public float acceleration = 1;
    public float rotationSpeed = 1;
    public float maxVelocity = 5f;
    //the min distance the fish keeps from the surface
    public float distanceFromSurface = 1f;
    public float distanceFromBottom = 0.5f;
    public float BottomResetHeight = 10f;
    public float bottomBump = 2f;

    //the distance the fish checks for objects infront of it, so it has to turn
    public float turnDistance = 10f;
    public float RandomTurnIntervallBase = 20f;
    public float RandomTurnAddMax = 60;
    public float turningMultiplier = 2;
    public waves waterScript;


    public bool isHostile = false;
    public float followRange = 10f;
    public int DetectionWidth = 2;
    public float detectionSpacing = 0.5f;
    public float detectionRange;
    public float chasingAddVelocity = 10f;
    public Transform player;
    public LayerMask groundAndPlayer;

    private bool hasDetected = false;


    private Rigidbody rb;

    private bool hitBottom = false;
    private bool isTurning = false;
    private float timeUntilNextTurn = 20;
    private Vector3 turningTarget;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    //this script makes fish roam
    void Update()
    {
        //move forward

        if(waterScript.GetHeight(transform.position) > transform.position.y - waterScript.transform.position.y)
        {
   
            rb.useGravity = false;

            Debug.DrawLine(transform.position, transform.position + Vector3.up * distanceFromSurface, Color.red);

            // keeps the fish from going to high

            if (detection() || hasDetected)
            {
                if (Vector3.Distance(player.position, transform.position) > followRange)
                    hasDetected = false;
                else
                    hasDetected = true;

                print("detected");
                //rotate towards player over time

                Vector3 rotaitionTarget = Vector3.MoveTowards(-transform.right, (player.position - transform.position).normalized, rotationSpeed * 4 * Time.deltaTime);
                transform.rotation = Quaternion.FromToRotation(-transform.right, rotaitionTarget) * transform.rotation;



            }
            else if (waterScript.GetHeight(transform.position) - distanceFromSurface < transform.position.y - waterScript.transform.position.y )
            {
                Vector3 rotaitionTarget = Vector3.MoveTowards(transform.up, -transform.right, rotationSpeed*4 * Time.deltaTime);
                transform.rotation = Quaternion.FromToRotation(transform.up, rotaitionTarget) * transform.rotation;

                
            }
            //keep the fish from goint to low
            else if(Physics.Raycast(transform.position, Vector3.down, distanceFromBottom) || hitBottom)
            {
                hitBottom = true;
                Vector3 rotaitionTarget = Vector3.MoveTowards(-transform.right, Vector3.up, rotationSpeed*5 * Time.deltaTime);
                transform.rotation = Quaternion.FromToRotation(-transform.right, rotaitionTarget) * transform.rotation;

                rb.position = rb.position + Vector3.up*bottomBump;

                if(!Physics.Raycast(transform.position, Vector3.down, BottomResetHeight))
                {
                    hitBottom = false;
                }
            }
            else
            {
                Vector3 rotaitionTarget = Vector3.MoveTowards(transform.up, Vector3.up, rotationSpeed * Time.deltaTime);
                transform.rotation = Quaternion.FromToRotation(transform.up, rotaitionTarget) * transform.rotation;
            }

            //turning
            if ((Physics.Raycast(transform.position, -transform.right, turnDistance) || timeUntilNextTurn <= 0) && !isTurning){
                isTurning = true;
                //decides if we are turning left or right, remember that the model has a weird orientation so forward is right
                if(Random.Range(0,1) == 0)
                {
                    turningTarget = transform.forward;
                }
                else
                {
                    turningTarget = -transform.forward;
                }


            }
            else if(isTurning)
            {
                Vector3 rotaitionTarget = Vector3.MoveTowards(-transform.right, turningTarget, rotationSpeed* turningMultiplier * Time.deltaTime);
                transform.rotation = Quaternion.FromToRotation(-transform.right, rotaitionTarget) * transform.rotation;

                if(-transform.right == turningTarget)
                {
                    isTurning = false;
                    timeUntilNextTurn = RandomTurnIntervallBase + Random.Range(0, RandomTurnAddMax);
                }
            }



            timeUntilNextTurn -= 1 * Time.deltaTime;


            if (!hasDetected)
            {
                if (rb.velocity.magnitude < maxVelocity)
                {
                    //the fish is turned sideways so therefore left is forward
                    rb.velocity = Vector3.MoveTowards(-transform.right * maxVelocity, rb.velocity, acceleration * Time.deltaTime);
                }
            }
            else
            {
                if (rb.velocity.magnitude < maxVelocity+ chasingAddVelocity)
                {
                    //the fish is turned sideways so therefore left is forward
                    rb.velocity = Vector3.MoveTowards(-transform.right * (maxVelocity +chasingAddVelocity), rb.velocity, acceleration * Time.deltaTime);
                }
            }


        }
        else
        {
            rb.useGravity = true;
        }
       

        
    }

    //doesn't work
    //direction also has to be fixed
    private bool detection()
    {
        Vector3 direction = -transform.right;
        Vector3 spacing = transform.forward * detectionSpacing;

        direction = direction - spacing *DetectionWidth;
 
        RaycastHit hit;
        for (int x = -DetectionWidth;  x <= DetectionWidth; x++)
        {
            Debug.DrawLine(transform.position, transform.position + direction * detectionRange, Color.red);
            if (Physics.Raycast(transform.position, direction, out hit, detectionRange, groundAndPlayer))
            {
                if(hit.transform.tag == "Player")
                    return true;
            }

            direction += spacing;
        }


        return false;
    }
}
