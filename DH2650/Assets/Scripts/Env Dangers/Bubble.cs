using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public float lifeTime = 5;
    public float travelSpeed = 1;
    public float sizeSpeed = 1;
    public float damage = 1;
    public float maxSize = 5;

    // Update is called once per frame
    void Update()
    {
        // If not max size, then scale
        if(transform.localScale.x < maxSize)
        {
            transform.localScale = new Vector3(
                transform.localScale.x + sizeSpeed * Time.deltaTime,
                transform.localScale.y + sizeSpeed * Time.deltaTime,
                transform.localScale.z + sizeSpeed * Time.deltaTime);
        }

        transform.position = new Vector3(
            transform.position.x,
            transform.position.y + travelSpeed * Time.deltaTime,
            transform.position.z);       
    }

    public void startTimer()
    {
        StartCoroutine(startTime());
    }

    private IEnumerator startTime()
    {
        yield return new WaitForSeconds(lifeTime);
        GameObject.Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        playerHealth.TakeDamage(damage);
    }

    private void OnTriggerStay(Collider other)
    {
        playerHealth.TakeDamage(damage);
    }
}
