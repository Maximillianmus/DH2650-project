using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    public Animator anim;
    public bool IsOpen = false;
    private AudioSource source;
    GameObject chestBar;

    void Start()
    {
        source = GetComponent<AudioSource>();
        chestBar = GameObject.Find("ChestBar");
    }

    public void SetOpen()
    {
        IsOpen = true;
        Debug.Log("trigger open");
        anim.SetTrigger("Open");
    }

    public override void Interact(OffHand offHand)
    {
        OpenChest();
    }

    private void OpenChest()
    {
        // If button is not pressed already, the player can press it
        if (IsOpen) return;
        
        IsOpen = true;
        anim.SetTrigger("Open");
        source.Play();
        if (chestBar)
        {
            chestBar.GetComponent<ChestBar>().UpdateOpenCount();
        }
    }


}
