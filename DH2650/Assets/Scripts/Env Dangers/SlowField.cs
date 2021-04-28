using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowField : Activation
{
    public float moveSpeed;
    public bool activated = false;
    public bool growing = true;
    [Header("Options")]
    public bool shrinkBack = false;
    public float maxScale = 10;
    public float minScale = 0;
    public float speed = 5;
    public float delay = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
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

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        playerHealth.TakeDamage(damage);
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
