using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTouch : MonoBehaviour
{
    public float damage;
    public PlayerHealth playerHealth;


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
