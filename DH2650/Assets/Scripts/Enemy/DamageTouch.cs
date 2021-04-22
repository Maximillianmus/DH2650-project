using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTouch : MonoBehaviour
{

    [Header("This script damages the player")]
    public float damage;
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            DealDamage(damage);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            DealDamage(damage);
        }
    }

    void DealDamage(float damage)
    {
        playerHealth.TakeDamage(damage);
    }
}
