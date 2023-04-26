using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string interactableName;
    public bool canBeInteractedWith = true;
    private bool playerInRange = false;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
    }

    public virtual void Interact()
    {
        Debug.Log("i was interacted with");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("InteractZone") && canBeInteractedWith)
        {
            //Debug.Log("found sth interactable");
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("InteractZone"))
        {
            //Debug.Log("left Interactable Range");
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (canBeInteractedWith && playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerMovement.DoAction("GetItem", 1.5f);
                Interact();
                //Debug.Log("I interacted with " + interactableName);
            }
        }
    }
}
