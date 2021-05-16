using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinScore : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetBar(float percent)
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        //rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, -100 + 200 * percent);
        //rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, 100 - 200 * percent);
        rectTransform.offsetMin = new Vector2(-100 + 200 * percent, 0);
        rectTransform.offsetMax = new Vector2(-100 + 200 * percent, 0);
    }

}
