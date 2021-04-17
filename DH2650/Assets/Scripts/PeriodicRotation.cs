using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicRotation : MonoBehaviour
{

    public float halfPeriodTime = 1;
    private float time;
    private float timeReset;

    public float MaxAngle = 90;

    public bool xAxis;
    public bool yAxis;
    public bool zAxis;

    public float Add = 1;

    private float xAngle = 0;
    private float yAngle = 0;
    private float zAngle = 0;


    void Start()
    {
        timeReset = halfPeriodTime / (MaxAngle/Mathf.Abs(Add));
        time = timeReset;
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            time = timeReset;
            Vector3 currentRotation = Vector3.zero;
            if (xAxis)
            {

                if (xAngle >= MaxAngle)
                {
                    Add = -Add;
                    xAngle = 0;
                }
                else
                {
                    currentRotation.x = Add;

                    xAngle += Mathf.Abs(Add);
                }

            }

            if (yAxis)
            {
                if (yAngle == MaxAngle)
                {
                    Add = -Add;
                    yAngle = 0;
                }
                else
                {
                    currentRotation.y = Add;

                    yAngle += Mathf.Abs(Add);
                }
            }

            if (zAxis)
            {
                if (zAngle == MaxAngle)
                {
                    Add = -Add;
                    zAngle = 0;
                }
                else
                {
                    currentRotation.z = Add;

                    zAngle += Mathf.Abs(Add);
                }
            }



            transform.Rotate(currentRotation);
        }
    }
        
}
