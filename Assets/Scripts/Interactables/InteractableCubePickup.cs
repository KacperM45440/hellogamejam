using System.Collections;
using UnityEngine;

public class InteractableCubePickup : InteractablePickup
{
    public GameObject parentPlatform;
    [SerializeField] private Animator animatorRef;
    [SerializeField] private CubePuzzleController puzzleControllerRef;
    private bool canBePickedUp = false;

    public void Start()
    {
        globalPickupAudioSource = puzzleControllerRef.audioSource;
        if (parentPlatform == null)
        {
            animatorRef.speed = 0;
        }
        StartCoroutine(WaitUntilPickupable());
    }

    public void SpawnedAnimated()
    {
        animatorRef.speed = 1;
    }

    public override void Interact()
    {
        if (puzzleControllerRef.puzzleComplete || !canBePickedUp)
        {
            return;
        }
        if (parentPlatform != null)
        {
            parentPlatform.GetComponent<InteractableCubePlatform>().PickedUpCube();
            parentPlatform = null;
        }
        base.Interact();
    }

    IEnumerator WaitUntilPickupable()
    {
        yield return new WaitForSeconds(1.5f);
        canBePickedUp = true;
    }
}
