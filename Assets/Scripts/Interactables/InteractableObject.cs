using System.Collections;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public PlayerReferences PlayerReferencesRef { get; private set; }
    public string InteractableName { get; private set; }

    [HideInInspector] public PlayerMovement PlayerMovement { get; private set; }
    [HideInInspector] public PlayerEquipment PlayerEquipmentRef { get; private set; }

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pickupAudio;
    [SerializeField] private float interactionCooldownTime = 1;
    [SerializeField] private bool canBeInteractedWith = true;
    [SerializeField] private bool playerInRange = false;
    [SerializeField] private bool defaultAnimation = true;
    
    private void Start()
    {
        InitializeReferences();
    }
    public void Update()
    {
        InteractionLogic();
    }

    public virtual void Interact()
    {
        //Leave empty; enforce implementation on child class
    }

    public void EnableInteraction()
    {
        canBeInteractedWith = true;
    }

    public void DisableInteraction()
    {
        canBeInteractedWith = false;
    }
    private void InitializeReferences()
    {
        PlayerEquipmentRef = PlayerReferencesRef.GetPlayerEquipment();
        PlayerMovement = PlayerReferencesRef.GetPlayerMovement();
        audioSource = GetComponent<AudioSource>();
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
                PlayerMovement.StopAndPlayAnimation("GetItem", 1.5f);
            }
            
            audioSource.clip = pickupAudio;
            audioSource.Play();
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
