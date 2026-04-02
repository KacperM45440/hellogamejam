using System.Collections;
using UnityEngine;

public class InteractableDoorKey : InteractableObject
{
    [SerializeField] private AudioClip tryOpenClip;
    [SerializeField] private AudioClip openClip;

    private PlayerEquipment playerEquipmentRef;
    private PlayerMovement playerMovementRef;
    private AudioSource audioSourceRef;

    private void Start()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        playerEquipmentRef = PlayerReferencesRef.GetPlayerEquipment();
        playerMovementRef = PlayerReferencesRef.GetPlayerMovement();
        audioSourceRef = GetComponent<AudioSource>();
    }

    public override void Interact() 
    {
        bool isKey = playerEquipmentRef.DoesItemExist("Key");

        if (!isKey)
        {
            audioSourceRef.clip = tryOpenClip;
            audioSourceRef.Play();
            playerMovementRef.StopAndPlayAnimation("Open", 1f);
        }
        else
        {
            playerMovementRef.StopAndPlayAnimation("GetItem", 1f);
            audioSourceRef.clip = openClip;
            DropKey();
            //transform.position = new Vector3(-4.423f, 1.646f, 2.555f);
            //transform.Rotate(new Vector3(0, -91.014f, 0));
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        transform.gameObject.SetActive(false);
    }

    private void DropKey()
    {
        playerEquipmentRef.DropItem("Key");
    }
}
