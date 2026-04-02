using System.Collections;
using UnityEngine;

public class InteractableDoor : InteractableObject
{
    [SerializeField] private PlayerReferences playerReferencesRef;
    [SerializeField] private PlayerEquipment playerEquipmentRef;
    [SerializeField] private AudioSource audioSourceRef;
    [SerializeField] private AudioClip tryOpenClip;
    [SerializeField] private AudioClip axeHitClip;
    [SerializeField] private AudioClip destroyClip;
    //[SerializeField] private Transform planks;
    [SerializeField] private Transform[] planks;

    private int hitToDestroyCount = 3;

    private void Start()
    {
        audioSourceRef = GetComponent<AudioSource>();
    }

    public override void Interact() 
    {
        bool isAxe = playerEquipmentRef.DoesItemExist("Hoeaxe");

        if (!isAxe)
        {
            audioSourceRef.clip = tryOpenClip;
            audioSourceRef.Play();
            playerReferencesRef.GetPlayerMovement().StopAndPlayAnimation("Open", 1f);
        }

        else 
        {
            playerReferencesRef.GetPlayerMovement().StopAndPlayAnimation("Axe", 1f);
            if (hitToDestroyCount > 1)
            {
                audioSourceRef.clip = axeHitClip;

            }
            if (hitToDestroyCount >= 1)
            {
                StartCoroutine(DropPlank(0.5f, hitToDestroyCount - 1));
            }
            hitToDestroyCount--;
        }

        if (hitToDestroyCount <= 0) 
        {
            DropAxe();
            Destroy(gameObject, 1.8f);
        }
    }

    private void DropAxe()
    {
        playerEquipmentRef.DropItem("Hoeaxe");
    }

    private IEnumerator DropPlank(float time, int index) 
    { 
        yield return new WaitForSeconds(time);
        if (index == 0) {
            audioSourceRef.clip = destroyClip;
        }
        audioSourceRef.Play();
        Transform plank = planks[index];
        Rigidbody rb = plank.gameObject.GetComponent<Rigidbody>();
        plank.gameObject.layer = 12;
        rb.isKinematic = false;
        rb.useGravity = true;
        plank.parent = null;
    }
}
