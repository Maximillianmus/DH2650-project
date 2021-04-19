using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnragedDonut : MonoBehaviour
{

    private Vector3 defaultRotation;
    public Vector3 enragedVector;
    public bool isEnraged;
    // Start is called before the first frame update
    void Start()
    {
        defaultRotation = transform.localRotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnraged)
        {
            transform.localRotation = Quaternion.Euler(enragedVector);
        }
        else
            transform.localRotation = Quaternion.Euler(defaultRotation);
    }
}
