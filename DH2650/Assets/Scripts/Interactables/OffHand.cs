using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OffHand : MonoBehaviour
{
    [SerializeField] Rigidbody playerRb;
    [SerializeField] Transform m_camera;
    [SerializeField] float pickUpRange = 2f;
    [SerializeField] LayerMask whatIsInteractable;
    [SerializeField] Text interactText; // For UI
    public int GroundLayer;
    [SerializeField] KeyCode InteractButton;
    public bool slotFull;
    public GameObject heldItem;

    PlayerHealth playerHealth;

    // for throwing items
    private bool keyDown = false;
    private float startTime = 0;
    [SerializeField] float throwForce = 80;

    private void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit; 
        bool performedAction = false;

        // Keep track of when player pressed interactButton (For throwing)
        if(!keyDown && Input.GetKeyDown(InteractButton))
        {
            keyDown = true;
            startTime = Time.time;
        }

        // Reset what interact text
        interactText.text = "";

        if (PauseMenu.AtSavePoint)
        {
            interactText.text = "Hold " + InteractButton + " to save progress";
        }

        // Determine what player is looking at
        if (Physics.Raycast(m_camera.position, m_camera.forward, out hit, pickUpRange, whatIsInteractable)){

            // I have seperated these in case we might want to do different things based on what player is interacting with

            if(hit.collider.tag == "Item")
            {
                interactText.text = "Press " + InteractButton + " to pick up";
                if(Input.GetKeyDown(InteractButton))
                {
                    hit.transform.gameObject.GetComponent<Interactable>().Interact(this);
                    keyDown = false;
                    performedAction = true;
                }
            }
            else if(hit.collider.tag == "Container")
            {
                interactText.text = "Press " + InteractButton + " to interact";
                if(Input.GetKeyDown(InteractButton))
                {
                    hit.transform.gameObject.GetComponent<Interactable>().Interact(this);
                    keyDown = false;
                    performedAction = true;
                }
            }
            
            else if(hit.collider.tag == "Interactable")
            {
                interactText.text = "Press " + InteractButton + " to interact";
                if(Input.GetKeyDown(InteractButton))
                {
                    hit.transform.gameObject.GetComponent<Interactable>().Interact(this);
                    keyDown = false;
                    performedAction = true;
                }
            }

            /**
             *  Currently (04-26) used for campfire healing
             */
            else if (hit.collider.tag == "HealObject")
            {
                interactText.text = "Press " + InteractButton + " to interact";
                if (Input.GetKeyDown(InteractButton))
                {
                    Heal(playerHealth.maxHealth);

                    keyDown = false;
                    performedAction = true;
                }
            }

        }
        // If we have not done any other action, hold an item and release E. Then we drop it.
        if(!performedAction && slotFull && keyDown && Input.GetKeyUp(InteractButton))
        {
            keyDown = false;
            float elapsedTime = Time.time - startTime;
            // Add a cap, holding interactButton for longer than 0.3 does not do more than just 0.3
            if(elapsedTime > 0.3)
            {
                elapsedTime = 0.3f;
            }
            DropItem(elapsedTime);
        }
    }

    private void DropItem(float elapsedTime)
    {
        slotFull = false;
        heldItem.transform.SetParent(null);

        // Restore collisions and physics and stuff
        Rigidbody rb = heldItem.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        // If player has not tapped, then apply a force to the object
        if(elapsedTime > 0.1)
        {
            Vector3 force = m_camera.forward * (throwForce * (elapsedTime));
            rb.AddForce(force, ForceMode.Impulse);
        }

        rb.velocity += playerRb.velocity;

        heldItem.layer = GroundLayer;

        Collider coll = heldItem.GetComponent<Collider>();
        coll.isTrigger = false;

        heldItem = null;
    }

    public void Heal(float amount)
    {
        playerHealth.LoadHealth(amount);
    }

}
