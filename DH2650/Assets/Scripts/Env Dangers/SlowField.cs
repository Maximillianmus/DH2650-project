using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowField : Activation
{
    private PlayerMovement playerMovement;
    public bool activated = false;
    public bool growing = true;
    [Header("Constant = Will be the same size forever")]
    public bool constant = true;
    [Header("Options")]
    public bool shrinkBack = false;
    public float maxScale = 10;
    public float minScale = 0;
    public float speed = 5;
    public float delay = 0;
    [Header("Speed Options, changes time instead due to Player Rigidbody")]
    public float timeModifier;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        if(constant)
        {
            transform.localScale = new Vector3(maxScale, maxScale, maxScale);
        }
        else
        {
            transform.localScale = new Vector3(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (constant)
        {
            if(activated)
            {
                transform.localScale = new Vector3(maxScale, maxScale, maxScale);
            } else
            {
                transform.localScale = new Vector3(0, 0, 0);
            }
        }
        else
        {
            if (activated)
            {
                if (growing)
                {
                    // Increase the scale in all directions
                    if (transform.localScale.x < maxScale)
                    {
                        transform.localScale = new Vector3(
                            transform.localScale.x + speed * Time.deltaTime,
                            transform.localScale.y + speed * Time.deltaTime,
                            transform.localScale.z + speed * Time.deltaTime);
                    }
                    else
                    {
                        if (shrinkBack)
                        {
                            growing = false;
                        }
                        else
                        {
                            transform.localScale = new Vector3(0, 0, 0);
                            activated = false;
                            StartCoroutine(activateAfterDelay());
                        }
                    }
                }
                else
                {
                    // Reduce the scale in all directions
                    if (transform.localScale.x > minScale)
                    {
                        transform.localScale = new Vector3(
                            transform.localScale.x - speed * Time.deltaTime,
                            transform.localScale.y - speed * Time.deltaTime,
                            transform.localScale.z - speed * Time.deltaTime);
                    }
                    else
                    {
                        activated = false;
                        growing = true;
                        StartCoroutine(activateAfterDelay());
                    }
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Time.timeScale = timeModifier;
        
    }

    private void OnTriggerExit(Collider other)
    {
        Time.timeScale = 1f;
    }

    private IEnumerator activateAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        activated = true;
    }

    public override void Activate()
    {
        activated = true;
        growing = true;
    }

    public override void DeActivate()
    {
        activated = false;
        transform.localScale = new Vector3(0, 0, 0);
    }
}
