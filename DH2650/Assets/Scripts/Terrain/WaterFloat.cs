using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFloat : MonoBehaviour
{
    public float AirDrag = 1;
    public float WaterDrag = 10;
    public Transform[] FloatPoints;
    public bool AttachToSurface;
    public float FloatDistance = 1f;

    private Rigidbody rb;
    private waves wavesScript;

    private float WaterLine;
    private Vector3[] WaterLinePoints;

    private Vector3 centerOffset;
    private Vector3 smoothVectorRotation;
    private Vector3 TargetUp;
    //what the up of the object should be when floating 
    private Vector3 floatingUp;

    public Vector3 Center { get { return transform.position + centerOffset; } }


    // Start is called before the first frame update
    void Awake()
    {
        //this part has to be changed if several water sources are used
        wavesScript = FindObjectOfType<waves>();
        rb = transform.GetComponent<Rigidbody>();
        rb.useGravity = false;

        WaterLinePoints = new Vector3[FloatPoints.Length];
        for(int i = 0; i < FloatPoints.Length; i++)
        {
            WaterLinePoints[i] = FloatPoints[i].position;
        }
        centerOffset = GetCenter(WaterLinePoints) - transform.position;


        Vector3 currentUp, floatingNormal;
        float upAngle = 0;
        currentUp = transform.up;
        floatingNormal = GetNormal(WaterLinePoints);

        upAngle = Vector3.Angle(currentUp, floatingNormal);

        floatingUp = Quaternion.FromToRotation(floatingNormal, Vector3.up) * currentUp;

    }




    // Update is called once per frame
    void Update()
    {
        //checks if the object is close enough to the water for floating to be relevant
        if(transform.position.y - (wavesScript.GetHeight(transform.position) + wavesScript.transform.position.y) < FloatDistance)
        {
            rb.useGravity = false;
            var newWaterLine = 0f;
            var pointUnderWater = false;

            //avg position of waterline
            for (int i = 0; i < FloatPoints.Length; i++)
            {
                WaterLinePoints[i] = FloatPoints[i].position;
                WaterLinePoints[i].y = wavesScript.GetHeight(FloatPoints[i].position) + wavesScript.transform.position.y;
                newWaterLine += WaterLinePoints[i].y / FloatPoints.Length;
                if (WaterLinePoints[i].y > FloatPoints[i].position.y)
                    pointUnderWater = true;
            }
            var waterLineDelta = newWaterLine - WaterLine;
            WaterLine = newWaterLine;

            var gravity = Physics.gravity;
            rb.drag = AirDrag;
            rb.angularDrag = 0.2f;
            if (WaterLine > Center.y)
            {
                rb.drag = WaterDrag;
                if (AttachToSurface)
                {
                    rb.position = new Vector3(rb.position.x, WaterLine - centerOffset.y, rb.position.z);
                }
                else
                {
                    gravity = -Physics.gravity;
                    transform.Translate(Vector3.up * waterLineDelta * 0.9f);
                }
            }

            rb.AddForce(gravity * Mathf.Clamp(Mathf.Abs(WaterLine - Center.y), 0, 1));

            if (pointUnderWater)
            {

                //remove shaking when close to target
                if (Vector3.Angle(transform.up, floatingUp) < 5)
                    TargetUp = floatingUp;
                else if (Vector3.Angle(transform.up, -floatingUp) < 5)
                    TargetUp = -floatingUp;

                else
                {
                    //we need to find out wich side is closer
                    if (Vector3.Angle(transform.up, floatingUp) > 90)
                        TargetUp = Vector3.SmoothDamp(transform.up, -floatingUp, ref smoothVectorRotation, 0.2f);
                    else
                        TargetUp = Vector3.SmoothDamp(transform.up, floatingUp, ref smoothVectorRotation, 0.2f);
                }

                rb.rotation = Quaternion.FromToRotation(transform.up, TargetUp) * rb.rotation;

            }
        }
        else
        {
            rb.useGravity = true;
            rb.angularDrag = 0.05f;
            rb.drag = 0;
        }
        
    }


    public static Vector3 GetCenter(Vector3[] points)
    {
        var center = Vector3.zero;
        for (int i = 0; i < points.Length; i++)
        {
            center += points[i] / points.Length;
        }
        return center;
    }


    public static Vector3 GetNormal(Vector3[] points)
    {
        //if there is not enough points
        if(points.Length < 3)
        {
            return Vector3.up;
        }

        //fitting points to a plane with algorithm from http://www.ilikebigbits.com/2015_03_04_plane_from_points.html
        //it uses the determinant
        var center = GetCenter(points);
        float xx = 0f, xy = 0f, xz = 0f, yy = 0f, yz = 0f, zz = 0f;

        for (int i = 0; i < points.Length; i++)
        {
            var r = points[i] - center;
            xx += r.x * r.x;
            xy += r.x * r.y;
            xz += r.x * r.z;
            yy += r.y * r.y;
            yz += r.y * r.z;
            zz += r.z * r.z;
        }

        var det_x = yy * zz - yz * yz;
        var det_y = xx * zz - xz * xz;
        var det_z = xx * yy - xy * xy;

        if (det_x > det_y && det_x > det_z)
            return new Vector3(det_x, xz * yz - xy * zz, xy * yz - xz * yy).normalized;
        if (det_y > det_z)
            return new Vector3(xz * yz - xy * zz, det_y, xy * xz - yz * xx).normalized;
        else
            return new Vector3(xy * yz - xz * yy, xy * xz - yz * xx, det_z).normalized;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (FloatPoints == null)
            return;

        for (int i = 0; i < FloatPoints.Length; i++)
        {
            if (FloatPoints[i] == null)
                continue;

            if (wavesScript != null)
            {

                //draw cube
                Gizmos.color = Color.red;
                Gizmos.DrawCube(WaterLinePoints[i], Vector3.one * 0.3f);
            }

            //draw sphere
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(FloatPoints[i].position, 0.1f);

        }

        //draw center
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector3(Center.x, WaterLine, Center.z), Vector3.one * 1f);
            Gizmos.DrawRay(new Vector3(Center.x, WaterLine, Center.z), TargetUp * 1f);
        }
    }

}
