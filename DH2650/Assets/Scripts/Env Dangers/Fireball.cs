using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float speed = 1;
    public float damage = 1;
    public float aliveIfWithin = 10;
    public float splashRadius = 1;

    // Update is called once per frame
    void Update()
    {
        // Fireball is only alive if it is close enough to the player
        if(Vector3.Distance(playerHealth.transform.position, transform.position) <= aliveIfWithin)
        {
            transform.LookAt(playerHealth.transform);
            transform.position = Vector3.MoveTowards(transform.position, playerHealth.transform.position, speed * Time.deltaTime);
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }

    // If fireball collides with anything, it explodes dealing damage to the player if they are wihin radius.
    private void OnCollisionEnter(Collision collision)
    {
        if(Vector3.Distance(collision.transform.position, playerHealth.transform.position) <= splashRadius)
        {
            playerHealth.TakeDamage(damage);
        }
        GameObject.Destroy(gameObject);
    }

}
