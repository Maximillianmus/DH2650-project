using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private PauseMenu pauseMenu;
    public AudioClip EnterSound;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            source.PlayOneShot(EnterSound);
            pauseMenu.ActivateSaveButton();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            source.PlayOneShot(EnterSound);
            pauseMenu.InactivateSaveButton();
        }
    }
}
