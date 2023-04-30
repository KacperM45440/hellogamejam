using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string interactableName;
    public bool canBeInteractedWith = true;
    private bool playerInRange = false;
    private PlayerMovement playerMovement;
    [SerializeField] private AudioClip pickupAudio;
    [SerializeField] private float interactionCooldown = 1;
    private AudioSource audioSource;
    

    private void Awake()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
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
                audioSource.clip = pickupAudio;
                audioSource.Play();
                Interact();
                canBeInteractedWith = false;
                if (interactionCooldown > 0)
                {
                    StartCoroutine(Cooldown());
                }
                //Debug.Log("I interacted with " + interactableName);
            }
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(interactionCooldown);
        canBeInteractedWith = true;
    }
}
