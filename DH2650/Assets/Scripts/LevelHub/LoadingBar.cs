using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingBar : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetProgress(float progress)
    {
        var scale = new Vector3(progress, 1f, 1f);
        transform.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
