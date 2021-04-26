using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointVisibility : MonoBehaviour
{
    [Header("Makes the waypoint invisible once game is started", order = 0)]
    [Space(-10, order = 1)]
    [Header("Bad Design variable here so this Header is possible", order = 2)]
    public char badDesign;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }

}
