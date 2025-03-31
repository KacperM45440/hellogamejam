using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InteractableDoorKey : InteractableObject
{
    [SerializeField] private PlayerReferences playerReferencesRef;
    private AudioSource audioSourceRef;
    [SerializeField] private AudioClip tryOpenClip;
    [SerializeField] private AudioClip openClip;

    void Start()
    {
        audioSourceRef = GetComponent<AudioSource>();
    }

    public override void Interact() 
    {
        bool isKey = PlayerEquipmentRef.DoesItemExist("Key");

        if (!isKey)
        {
            audioSourceRef.clip = tryOpenClip;
            audioSourceRef.Play();
            playerReferencesRef.GetPlayerMovement().StopAndPlayAnimation("Open", 1f);
        }
        else
        {
            playerReferencesRef.GetPlayerMovement().StopAndPlayAnimation("GetItem", 1f);
            audioSourceRef.clip = openClip;
            DropKey();
            //transform.position = new Vector3(-4.423f, 1.646f, 2.555f);
            //transform.Rotate(new Vector3(0, -91.014f, 0));
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        transform.gameObject.SetActive(false);
    }

    private void DropKey()
    {
        PlayerEquipmentRef.DropItem("Key");
    }
}
