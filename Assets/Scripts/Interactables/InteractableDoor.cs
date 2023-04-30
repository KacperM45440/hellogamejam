using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableDoor : InteractableObject
{
    private AudioSource audioSourceRef;
    [SerializeField] private AudioClip tryOpenClip;
    [SerializeField] private AudioClip axeHitClip;
    [SerializeField] private AudioClip destroyClip;
    //[SerializeField] private Transform planks;
    [SerializeField] private Transform[] planks;

    private int hitToDestroyCount = 3;

    void Start()
    {
        audioSourceRef = GetComponent<AudioSource>();
    }

    public override void Interact() {
        bool isAxe = PlayerEquipment.Instance.isItemExist("Hoeaxe");

        if (!isAxe)
        {
            audioSourceRef.clip = tryOpenClip;
            audioSourceRef.Play();
            PlayerReference.Instance.playerMovement.DoAction("Open", 1f);
        }
        else {
            PlayerReference.Instance.playerMovement.DoAction("Axe", 1f);
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
        if (hitToDestroyCount <= 0) {
            DropAxe();
            Destroy(gameObject, 1f);
        }
    }

    void DropAxe()
    {
        for (int i = 0; i < 3; i++)
        {
            if (PlayerEquipment.Instance.heldObjectNames[i].Equals("Hoeaxe"))
            {
                PlayerEquipment.Instance.heldObjectNames[i] = "";
                PlayerEquipment.Instance.heldObjectSprites[i] = null;
                PlayerEquipment.Instance.slots[i].GetComponent<Image>().sprite = null;

                Color c = PlayerEquipment.Instance.slots[i].GetComponent<Image>().color;
                c.a = 0;
                PlayerEquipment.Instance.slots[i].GetComponent<Image>().color = c;
            }
        }
    }

    IEnumerator DropPlank(float time, int index) { 
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
