using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : Activation
{
    public GameObject fireball;
    public PlayerHealth playerHealth;
    [Header("Options")]
    public bool activated = false;
    public float delay = 1;
    public float range = 10;
    public float fireballSpeed = 1;
    public float fireballDamage = 1;
    public float fireballAliveIfWithin = 10;
    public float fireballSplashRadius = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(activated)
        {
            if(Vector3.Distance(transform.position, playerHealth.transform.position) <= range)
            {
                spawnFireball();
                activated = false;
                StartCoroutine(activateAfterDelay());
            }
        }
    }

    private void spawnFireball()
    {
        GameObject obj = GameObject.Instantiate(fireball, transform.position, Quaternion.identity);
        Fireball f = obj.GetComponent<Fireball>();
        f.playerHealth = playerHealth;
        f.speed = fireballSpeed;
        f.damage = fireballDamage;
        f.aliveIfWithin = fireballAliveIfWithin;
    }

    private IEnumerator activateAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        activated = true;
    }

    public override void Activate()
    {
        activated = true;
    }

    public override void DeActivate()
    {
        activated = false;
    }
}
