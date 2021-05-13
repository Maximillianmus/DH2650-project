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

    //the distance the fish checks for objects infront of it, so it has to turn
    public float turnDistance = 10f;
    public float RandomTurnIntervallBase = 20f;
    public float RandomTurnAddMax = 60;
    public waves waterScript;

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
            


            //move up and down, keeps the fish from going to high
            if (Mathf.Abs(waterScript.GetHeight(transform.position) - transform.position.y - waterScript.transform.position.y) < distanceFromSurface)
            {
                Vector3 rotaitionTarget = Vector3.MoveTowards(transform.up, -transform.right, rotationSpeed * Time.deltaTime);
                transform.rotation = Quaternion.FromToRotation(transform.up, rotaitionTarget) * transform.rotation;
            }
            //keep the fish from goint to low
            else if(Physics.Raycast(transform.position, Vector3.down, distanceFromBottom) || hitBottom)
            {
                print("hej");
                Vector3 rotaitionTarget = Vector3.MoveTowards(-transform.right, transform.up, rotationSpeed * Time.deltaTime);
                transform.rotation = Quaternion.FromToRotation(-transform.right, rotaitionTarget) * transform.rotation;

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
                Vector3 rotaitionTarget = Vector3.MoveTowards(-transform.right, turningTarget, rotationSpeed * Time.deltaTime);
                transform.rotation = Quaternion.FromToRotation(-transform.right, rotaitionTarget) * transform.rotation;

                if(-transform.right == turningTarget)
                {
                    isTurning = false;
                    timeUntilNextTurn = RandomTurnIntervallBase + Random.Range(0, RandomTurnAddMax);
                }
            }



            timeUntilNextTurn -= 1 * Time.deltaTime;



            if (rb.velocity.magnitude < maxVelocity)
            {
                //the fish is turned sideways so therefore left is forward
                rb.velocity = Vector3.MoveTowards(-transform.right * maxVelocity, rb.velocity, acceleration * Time.deltaTime);
            }

        }
        else
        {
            rb.useGravity = true;
        }
       

        
    }

}
