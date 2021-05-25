using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOnTouchEnv : MonoBehaviour
{

    private PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        Collider collider = GameObject.Find("Player").GetComponent<CapsuleCollider>();
        if (other == collider)
        {
            DeathOnTouch();
        }
    }

    private void DeathOnTouch()
    {
        playerHealth.TakeDamage(playerHealth.currentHealth);
    }
}
