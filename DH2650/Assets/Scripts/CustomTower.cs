﻿using UnityEngine;
using System.Collections;

public class CustomTower : MonoBehaviour {


    public bool Catcher = false;
    public Transform shootElement;
    public Transform LookAtObj;    
    public GameObject bullet;
    public Vector3 impactNormal_2;
    public Transform target;
    public int dmg = 10;
    public float shootDelay;
    bool isShoot;
    public Animator anim_2;
    private float homeY;
    public AudioClip shootsound;
    private AudioSource source;
    public float lowVolumeRange = .1f;
    public float highVolumeRange = .3f;

    // for Catcher tower 

    void Start()
    {
        anim_2 = GetComponent<Animator>();
        homeY = LookAtObj.transform.localRotation.eulerAngles.y;
        source = GetComponent<AudioSource>();
    }


    void Update () {
        // Tower`s rotate

        if (target)
        {
            Vector3 dir = target.transform.position - LookAtObj.transform.position;
            dir.y = 0;
            Quaternion rot = Quaternion.LookRotation(dir);
            LookAtObj.transform.rotation = Quaternion.Slerp(LookAtObj.transform.rotation, rot, 5 * Time.deltaTime);

        }

        else
        {

            Quaternion home = new Quaternion(0, homeY, 0, 1);

            LookAtObj.transform.rotation = Quaternion.Slerp(LookAtObj.transform.rotation, home, Time.deltaTime);
        }


        // Shooting


        if (!isShoot)
        {
            StartCoroutine(shoot());

        }


        if (Catcher == true)
        {
            if (!target || target.CompareTag("Dead"))
            {

                StopCatcherAttack();
            }

        }

    }

    IEnumerator shoot()
    {
        isShoot = true;
        yield return new WaitForSeconds(shootDelay);


        if (target && Catcher == false)
        {
            float vol = Random.Range(lowVolumeRange, highVolumeRange);
            source.PlayOneShot(shootsound, vol);
            GameObject b = GameObject.Instantiate(bullet, shootElement.position, Quaternion.identity) as GameObject;
            b.GetComponent<CustomTowerBullet>().target = target;
            b.GetComponent<CustomTowerBullet>().twr = this;

        }

        if (target && Catcher == true)
        {
            anim_2.SetBool("Attack", true);
            anim_2.SetBool("T_pose", false);
        }


        isShoot = false;
    }



    void StopCatcherAttack()

        {                
            target = null;
            anim_2.SetBool("Attack", false);
            anim_2.SetBool("T_pose", true);        
        } 
          

}



