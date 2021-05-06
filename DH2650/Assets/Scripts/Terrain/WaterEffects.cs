using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;

public class WaterEffects : MonoBehaviour
{
    public float checkdistance = 10f;
    public LayerMask water;
    public bool debug = false;
    public Color fogColor;

    private Volume volume;
    private Transform playerHead;
    private RaycastHit hit;
    private Camera mainCamera;
    private waves waterScript;
    private UnderwaterBreath breathingScript;

    // Start is called before the first frame update
    void Awake()
    {
        playerHead = GameObject.FindGameObjectsWithTag("Head")[0].transform;
        volume = transform.GetComponent<Volume>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogMode = FogMode.ExponentialSquared;
        waterScript = transform.GetComponent<waves>();
        breathingScript = playerHead.parent.GetComponent<UnderwaterBreath>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 raystart = playerHead.transform.position + playerHead.up * (checkdistance+1f);

        if (debug)
        {
            Debug.DrawLine(playerHead.position, playerHead.position + playerHead.up * checkdistance, Color.cyan);
        }
        //if (Physics.Raycast(raystart, -playerHead.up, out hit, checkdistance, water))
        if(waterScript.GetHeight(playerHead.transform.position) > (playerHead.transform.position.y - transform.position.y))
        {
            volume.enabled = true;
            RenderSettings.fog = true;
            breathingScript.underwater = true;
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
            mainCamera.backgroundColor = fogColor;
        }
        else
        {
            volume.enabled = false;
            RenderSettings.fog = false;
            breathingScript.underwater = false;
            mainCamera.clearFlags = CameraClearFlags.Skybox;
        }
           
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Head")
            print("hello");
    }

}
