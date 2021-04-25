using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public string SoundName;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play(SoundName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
