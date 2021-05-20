using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : Activation
{
    [Header("Options")]
    public float travelSpeed;
    public float[] yLevels = new float[0];
    public bool isMoving = false;
    public int currentIndex = 0;

    void Update()
    {
        if (isMoving)
        {
            // If lavas y position is close enough to the current layer then stop movement
            if(yLevels[currentIndex] - 0.005 <= transform.localPosition.y && yLevels[currentIndex] + 0.005 >= transform.localPosition.y)
            {
                isMoving = false;
            }
            // if lava is below, move up
            else if(yLevels[currentIndex] > transform.localPosition.y)
            {
                transform.Translate(new Vector3(0, travelSpeed * Time.deltaTime, 0));
            }
            // if lava is above, move down
            else if(yLevels[currentIndex] < transform.localPosition.y)
            {
                transform.Translate(new Vector3(0, -travelSpeed * Time.deltaTime, 0));
            }
        }
    }

    // Each time lava gets activated it progresses to the next y level
    public override void Activate()
    {
        if (currentIndex < yLevels.Length-1)
        {
            isMoving = true;
            currentIndex++;
        }
    }

    // Each time lava gets deactivated it goes back to the previous y level
    public override void DeActivate()
    {  
        if(currentIndex > 0)
        {
            isMoving = true;
            currentIndex--;
        }
    }
}
