using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableCubePlatform : InteractableObject
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip putCube;
    //[SerializeField] private AudioClip takeCube;
    [SerializeField] private AudioClip noCubeInInventory;
    [SerializeField] private GameObject cubePrefab;

    [SerializeField] private Vector3 cubeSpot;

    public bool hasCube = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void Interact()
    {
        bool isCubeInInventory = PlayerEquipment.Instance.isItemExist("Cube");
        
        //powinno mieæ obwódkê tylko jak nie ma na tym skrzynki
        if(isCubeInInventory && !hasCube)
        {
            audioSource.clip = putCube;
            PlayerReference.Instance.playerMovement.DoAction("GetItem", 1.5f);
            DropCube();

            GameObject newCube = Instantiate(cubePrefab, transform);
            newCube.transform.localPosition = cubeSpot;
            InteractableCubePickup newCubeRef = newCube.GetComponent<InteractableCubePickup>();
            newCubeRef.parentPlatform = gameObject;
            //newCubeRef.WaitUntilPickupable(1); //TO NIE DZIA£A A POWINNO NADAÆ COOLDOWN SKRZYNCE ABY NIE SPAMOWAÆ
            hasCube = true;
            //TUTAJ POWINNO RÓWNIE¯ NADAWAÆ KOSTCE NATYCHMIASTOWO OUTLINE
        }
        audioSource.Play();
    }

    public void PickedUpCube()
    {
        hasCube = false;
        canBeInteractedWith = true;
    }

    void DropCube()
    {
        for (int i = 0; i < 3; i++)
        {
            if (PlayerEquipment.Instance.heldObjectNames[i].Equals("Cube"))
            {
                PlayerEquipment.Instance.heldObjectNames[i] = "";
                PlayerEquipment.Instance.heldObjectSprites[i] = null;
                PlayerEquipment.Instance.slots[i].GetComponent<Image>().sprite = null;

                Color c = PlayerEquipment.Instance.slots[i].GetComponent<Image>().color;
                c.a = 0;
                PlayerEquipment.Instance.slots[i].GetComponent<Image>().color = c;
                break;
            }
        }
    }
}
