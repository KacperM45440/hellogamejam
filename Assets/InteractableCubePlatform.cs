using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableCubePlatform : InteractableObject
{
    public int platformId;

    private AudioSource audioSourceRef;
    [SerializeField] private AudioClip putCube;
    //[SerializeField] private AudioClip takeCube;
    [SerializeField] private AudioClip noCubeInInventory;
    [SerializeField] private GameObject cubePrefab;

    [SerializeField] private Vector3 cubeSpot;

    public bool hasCube = false;
    private bool startsWithCube = false;
    private bool canBeInteracted = true;

    void Start()
    {
        audioSourceRef = GetComponent<AudioSource>();
        if (hasCube)
        {
            CubePuzzleController.instance.PlatformTurnOn(platformId);
            startsWithCube = true;
        }
    }

    public override void Interact()
    {
        if (startsWithCube || !canBeInteracted)
        {
            startsWithCube = false;
            return;
        }
        bool isCubeInInventory = PlayerEquipment.Instance.isItemExist("Cube");
        
        //powinno mieæ obwódkê tylko jak nie ma na tym skrzynki
        if(isCubeInInventory && !hasCube)
        {
            audioSourceRef.clip = putCube;
            PlayerReference.Instance.playerMovement.DoAction("GetItem", 1.5f);
            DropCube();

            GameObject newCube = Instantiate(cubePrefab, transform);
            newCube.transform.localPosition = cubeSpot;
            newCube.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            InteractableCubePickup newCubeRef = newCube.GetComponent<InteractableCubePickup>();
            newCubeRef.parentPlatform = gameObject;
            newCubeRef.SpawnedAnimated();
            hasCube = true;
            //TUTAJ POWINNO RÓWNIE¯ NADAWAÆ KOSTCE NATYCHMIASTOWO OUTLINE
            CubePuzzleController.instance.PlatformTurnOn(platformId);
        }
        audioSourceRef.Play();
        StartCoroutine(WaitUntilInteractable());
    }

    IEnumerator WaitUntilInteractable()
    {
        canBeInteracted = false;
        yield return new WaitForSeconds(1.5f);
        canBeInteracted = true;
    }

    public void PickedUpCube()
    {
        hasCube = false;
        canBeInteractedWith = true;
        CubePuzzleController.instance.PlatformTurnOff(platformId);
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
