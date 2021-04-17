using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHub : MonoBehaviour
{
    public GameObject levelMap;

    void ApplyMap()
    {
        //Texture2D texture = Resources.Load("Textures/map1") as Texture2D;
        //Material material = new Material(Shader.Find("Diffuse"));
        //material.mainTexture = texture;
        //levelMap.GetComponent<Renderer>().material = material;
    }

    // Start is called before the first frame update
    void Start()
    {
        ApplyMap();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
