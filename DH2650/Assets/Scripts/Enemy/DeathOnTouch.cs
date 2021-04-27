using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOnTouch : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;

    public void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    // If player collides with Spike, the player dies.
    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            playerHealth.TakeDamage(playerHealth.currentHealth);
        }
    }
}
