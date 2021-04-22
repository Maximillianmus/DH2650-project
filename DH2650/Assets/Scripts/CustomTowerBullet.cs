using UnityEngine;
using System.Collections;
using Unity.Mathematics;

public class CustomTowerBullet : MonoBehaviour {

    public float Speed;
    public Transform target;
    public GameObject impactParticle; // bullet impact
    
    public Vector3 impactNormal; 
    Vector3 lastBulletPosition; 
    public CustomTower twr;    
    float i = 0.05f; // delay time of bullet destruction
    private Vector3 startPosition;
    private Vector3 targetPosition;

    protected float Animation;

    Rigidbody m_Rigidbody;
    public float m_Speed = 10f;

    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (target)
        {
            if (math.any(targetPosition))
            {
            }
            else
            {
                targetPosition = target.position;
                Vector3 dir = targetPosition - transform.position;
                m_Rigidbody.AddForce((dir + new Vector3(0, 5f, 0)) * 100);
                Destroy(gameObject, 3);
            }
        }
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.transform == target)
        {
            target.GetComponent<PlayerHealth>().TakeDamage(twr.dmg);
            Destroy(gameObject, i);
            return;
        }
    }
}



