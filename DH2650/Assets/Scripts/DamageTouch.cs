using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTouch : MonoBehaviour
{
    GameObject Player;
    public int damage;
    public Collider collider;
    public PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Player")
        {
            DealDamage(damage);
        }
    }

    void DealDamage(int damage)
    {
        playerHealth.TakeDamage(damage);
    }
}
