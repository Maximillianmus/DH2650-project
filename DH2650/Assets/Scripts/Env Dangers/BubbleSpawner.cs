using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : Activation
{
    public GameObject bubble;
    public PlayerHealth playerHealth;
    private UnderwaterBreath airBar;
    [Header("Options")]

    public bool AirSpawner;

    public bool activated = false;
    public float delayAfterActivation = 0;
    public float maxRandomAdditionalDelay = 1;
    public float spawnDelay = 1;
    public float bubbleTravelSpeed = 1;
    public float bubbleSizeSpeed = 1;
    public float bubbleDamage = 1;
    public float bubbleLifeTime = 5;
    public float bubbleMaxSize = 5;


    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        airBar = FindObjectOfType<UnderwaterBreath>();
    }

    // Update is called once per frame
    void Update()
    {
        if(activated)
        {
            spawnBubble();
            activated = false;
            float randomDelay = Random.Range(0, maxRandomAdditionalDelay);
            StartCoroutine(activateAfterDelay(spawnDelay + randomDelay));
        }
    }

    private void spawnBubble()
    {
        GameObject obj = GameObject.Instantiate(bubble, transform.position, Quaternion.identity);
        Bubble b = obj.GetComponent<Bubble>();
        b.playerHealth = playerHealth;
        b.lifeTime = bubbleLifeTime;
        b.AirBubble = AirSpawner;
        b.air = airBar;
        b.travelSpeed = bubbleTravelSpeed;
        b.sizeSpeed = bubbleSizeSpeed;
        b.maxSize = bubbleMaxSize;
        b.damage = bubbleDamage;
        b.startTimer();
    }

    private IEnumerator activateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        activated = true;
    }


    public override void Activate()
    {
        if(delayAfterActivation > 0)
        {
            activateAfterDelay(delayAfterActivation);
        }
        else
        {
            activated = true;
        }
    }

    public override void DeActivate()
    {
        activated = false;
    }
}
