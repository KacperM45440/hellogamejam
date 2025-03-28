using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InteractableCubePlatform : InteractableObject
{
    public int platformId;

    [SerializeField] private CubePuzzleController puzzleControllerRef;
    [SerializeField] private PlayerReferences playerReferencesRef;
    private AudioSource audioSourceRef;
    [SerializeField] private AudioClip putCube;
    //[SerializeField] private AudioClip takeCube;
    [SerializeField] private AudioClip noCubeInInventory;
    [SerializeField] private GameObject cubePrefab;

    [SerializeField] private Vector3 cubeSpot;

    public bool hasCube = false;
    private bool startsWithCube = false;
    private bool canBeInteracted = true;

    private void Start()
    {
        audioSourceRef = GetComponent<AudioSource>();
        if (hasCube)
        {
            puzzleControllerRef.PlatformTurnOn(platformId);
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
            playerReferencesRef.GetPlayerMovement().StopAndPlayAnimation("GetItem", 1.5f);
            DropCube();

            GameObject newCube = Instantiate(cubePrefab, transform);
            newCube.transform.localPosition = cubeSpot;
            newCube.transform.localScale = new Vector3(1f, 1f, 1f);
            InteractableCubePickup newCubeRef = newCube.GetComponent<InteractableCubePickup>();
            newCubeRef.parentPlatform = gameObject;
            newCubeRef.SpawnedAnimated();
            hasCube = true;
            //TUTAJ POWINNO RÓWNIE¯ NADAWAÆ KOSTCE NATYCHMIASTOWO OUTLINE
            puzzleControllerRef.PlatformTurnOn(platformId);
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
        puzzleControllerRef.PlatformTurnOff(platformId);
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
