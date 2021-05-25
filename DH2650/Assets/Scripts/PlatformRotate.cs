using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotate : MonoBehaviour
{
    [Header("Rotation angle")]
    public float rotAngle;
    public float delay;
    [Header("Speed of spinning")]
    public float speed;
    public bool slowRotation = false;
    //public bool rotateAndStop = false;
    private float setDelay;
    private float rot;
    //private bool startRotate = false;
    //private bool stopRotate = false;

    public enum RotationAxis
    {
        All,
        Y,
        X,
        Z
    }

    public RotationAxis axis;

    // Start is called before the first frame update
    void Start()
    {
        setDelay = delay;
        rot = Time.deltaTime * speed * rotAngle;
    }

    // Update is called once per frame
    void Update()
    {

        delay -= Time.deltaTime;
        if (slowRotation)
        {
            if (delay < 0f)
            {

                rotateObject(axis);
                delay += setDelay;
            }
        }
        else
        {
            rotateObject(axis);
        }

        /*
        if (rotateAndStop)
        {
            if (startRotate)
            {
                rotateObject(axis);
                startRotate = false;
                delay = setDelay;
            }
            delay -= Time.deltaTime;
            if(delay < 0)
            {
                startRotate = true;
            }
        }*/

    }
    void rotateObject(RotationAxis axis)
    {
        switch (axis)
        {
            default:
            case RotationAxis.All:
                // Debug.Log("Rotating All");
                transform.Rotate(new Vector3(rot * rotAngle, rot * rotAngle, rot * rotAngle));
                break;

            case RotationAxis.X:
                //Debug.Log("Rotating X");
                transform.Rotate(new Vector3(rot * rotAngle, 0f, 0f));
                break;

            case RotationAxis.Y:
                //Debug.Log("Rotating Y");
                transform.Rotate(new Vector3(0f, rot * rotAngle, 0f));
                break;

            case RotationAxis.Z:
                //Debug.Log("Rotating Z");
                transform.Rotate(new Vector3(0f, 0f, rot * rotAngle));
                break;

        }
    }
}
