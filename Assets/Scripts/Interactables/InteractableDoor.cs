using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : InteractableObject
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip tryOpenClip;
    [SerializeField] private AudioClip axeHitClip;
    [SerializeField] private AudioClip destroyClip;

    private int hitToDestroyCount = 3;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }
    public override void Interact() {
        Debug.Log("JEB DRZWI");
        bool isAxe = PlayerEquipment.Instance.isItemExist("Hoeaxe");

        if (!isAxe)
        {
            audioSource.clip = tryOpenClip;
            PlayerReference.Instance.playerMovement.DoAction("TryOpen", 1f);
        }
        else {
            PlayerReference.Instance.playerMovement.DoAction("TryOpen", 1f);
            if (hitToDestroyCount > 1)
            {
               audioSource.clip = axeHitClip;

            }
            else {
               audioSource.clip = destroyClip;
            }
            hitToDestroyCount--;
        }
        audioSource.Play();
    }
}
