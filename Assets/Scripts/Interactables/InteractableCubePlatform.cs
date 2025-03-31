using System.Collections;
using UnityEngine;

public class InteractableCubePlatform : InteractableObject
{
    [SerializeField] private CubePuzzleController puzzleControllerRef;
    [SerializeField] private PlayerReferences playerReferencesRef;
    [SerializeField] private AudioSource platformAudioSource;
    [SerializeField] private AudioClip placeCubeSFX;
    [SerializeField] private AudioClip missingCubeSFX;
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Vector3 cubeSpot;

    private PlayerEquipment playerEquipmentRef;

    public int platformId;
    public bool hasCube = false;
    private bool startsWithCube = false;
    private bool canBeInteracted = true;

    private void Start()
    {
        InitializeReferences();
        InitializePlatforms();
    }

    private void InitializeReferences()
    {
        playerEquipmentRef = playerReferencesRef.GetPlayerEquipment();
    }

    private void InitializePlatforms()
    {
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
        bool isCubeInInventory = playerEquipmentRef.DoesItemExist("Cube");
        
        //Should only outline when a cube is not placed
        if (isCubeInInventory && !hasCube)
        {
            platformAudioSource.clip = placeCubeSFX;
            playerReferencesRef.GetPlayerMovement().StopAndPlayAnimation("GetItem", 1.5f);
            DropCube();

            GameObject newCube = Instantiate(cubePrefab, transform);
            newCube.transform.localPosition = cubeSpot;
            newCube.transform.localScale = new Vector3(1f, 1f, 1f);
            InteractableCubePickup newCubeRef = newCube.GetComponent<InteractableCubePickup>();
            newCubeRef.SetParentPlatform(gameObject);
            newCubeRef.SpawnedAnimated();
            hasCube = true;
            //Should outline here too
            puzzleControllerRef.PlatformTurnOn(platformId);
        }

        platformAudioSource.Play();
        StartCoroutine(WaitUntilInteractable());
    }

    public void PickedUpCube()
    {
        hasCube = false;
        EnableInteraction();
        puzzleControllerRef.PlatformTurnOff(platformId);
    }

    private void DropCube()
    {
        playerEquipmentRef.DropItem("Cube");
    }

    private IEnumerator WaitUntilInteractable()
    {
        canBeInteracted = false;
        yield return new WaitForSeconds(1.5f);
        canBeInteracted = true;
    }
}
