using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : InteractableObject
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip tryOpenClip;
    [SerializeField] private AudioClip axeHitClip;
    [SerializeField] private AudioClip destroyClip;
    //[SerializeField] private Transform planks;
    [SerializeField] private Transform[] planks;

    private int hitToDestroyCount = 3;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void Interact() {
        bool isAxe = PlayerEquipment.Instance.isItemExist("Hoeaxe");
        Debug.Log("JEB DRZWI: " + isAxe);

        if (!isAxe)
        {
            audioSource.clip = tryOpenClip;
            PlayerReference.Instance.playerMovement.DoAction("Open", 1f);
        }
        else {
            PlayerReference.Instance.playerMovement.DoAction("Axe", 1f);
            if (hitToDestroyCount > 1)
            {
               audioSource.clip = axeHitClip;

            }
            else {
               audioSource.clip = destroyClip;
            }
            Transform plank = planks[hitToDestroyCount - 1];
            Rigidbody rb = plank.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.right * 10f, ForceMode.Impulse);
            plank.gameObject.layer = 12;
            //Destroy(plank.GetChild(0));
            rb.isKinematic = false;
            rb.useGravity = true;
            plank.parent = null;

            hitToDestroyCount--;
        }
        audioSource.Play();
        if (hitToDestroyCount <= 0) {
            Destroy(gameObject, 1f);
        }
    }
}
