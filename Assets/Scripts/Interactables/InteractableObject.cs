using System.Collections;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public PlayerReferences PlayerReferencesRef;
    
    [SerializeField] private string interactableName;
    [SerializeField] private AudioClip pickupAudio;
    [SerializeField] private float interactionCooldownTime = 1;
    [SerializeField] private bool canBeInteractedWith = true;
    [SerializeField] private bool defaultAnimation = true;

    private PlayerMovement playerMovementRef;
    private PlayerEquipment playerEquipmentRef;
    private AudioSource interactableAudioSource;
    private bool playerInRange = false;

    private void Start()
    {
        InitializeReferences();
    }
    public void Update()
    {
        InteractionLogic();
    }
    private void InitializeReferences()
    {
        playerEquipmentRef = PlayerReferencesRef.GetPlayerEquipment();
        playerMovementRef = PlayerReferencesRef.GetPlayerMovement();
        interactableAudioSource = GetComponent<AudioSource>();
    }

    public string GetInteractableName()
    {
        return interactableName;
    }

    public virtual void Interact()
    {
        // Leave empty; enforce implementation on child class
    }

    public void EnableInteraction()
    {
        canBeInteractedWith = true;
    }

    public void DisableInteraction()
    {
        canBeInteractedWith = false;
    }

    public void PlaySound()
    {
        interactableAudioSource.Play();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("InteractZone") && canBeInteractedWith)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("InteractZone"))
        {
            playerInRange = false;
        }
    }

    private void InteractionLogic()
    {
        if (!canBeInteractedWith || !playerInRange)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (defaultAnimation)
            {
                playerMovementRef.StopAndPlayAnimation("GetItem", 1.5f);
            }
            
            interactableAudioSource.clip = pickupAudio;
            
            PlaySound();
            Interact();
            DisableInteraction();
           
            if (interactionCooldownTime > 0)
            {
                StartCoroutine(InteractionCooldown());
            }
        }
    }

    private IEnumerator InteractionCooldown()
    {
        yield return new WaitForSeconds(interactionCooldownTime);
        EnableInteraction();
    }
}
