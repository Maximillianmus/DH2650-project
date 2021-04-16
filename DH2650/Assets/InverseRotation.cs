using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseRotation : MonoBehaviour
{

    public Transform Target;
    public bool xAxis;
    public bool yAxis;
    public bool zAxis;



    // Update is called once per frame
    void Update()
    {
        Vector3 targetRot = Target.localRotation.eulerAngles;
        if (xAxis)
            targetRot.x = -targetRot.x;
        if (yAxis)
            targetRot.y = -targetRot.y;
        if (zAxis)
            targetRot.z = -targetRot.z;

        transform.localRotation = Quaternion.Euler(targetRot);
    }
}
