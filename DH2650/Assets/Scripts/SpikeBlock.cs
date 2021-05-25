using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBlock : MonoBehaviour
{

    public float initalOffset = 0;
    public float movementSpeed = 1;
    public float uppMultiplier = 0.5f;
    public float movementDistance = 1;
    public Vector3 movementDirection;


    private bool forward = true;
    private bool start = true;
    private Vector3 initialPos;
    private float timeSinceStart = 0;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            timeSinceStart += Time.deltaTime;
            if (timeSinceStart >= initalOffset)
            {
                start = false;
            }
        }
        else
        {
            if (forward)
            {
                transform.position = Vector3.MoveTowards(transform.position, initialPos + movementDirection * movementDistance, movementSpeed * Time.deltaTime);

                if(Vector3.Distance(transform.position, (initialPos + movementDirection * movementDistance)) < 0.0001f)
                {
                    forward = false;
                        
                }
            } 
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, initialPos, movementSpeed * uppMultiplier * Time.deltaTime);
                if (Vector3.Distance(transform.position, (initialPos)) < 0.0001f)
                {
                    forward = true;
                }
            }
              
        }


        
        
    }
}
