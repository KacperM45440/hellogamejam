using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCubePickup : InteractablePickup
{
    public GameObject parentPlatform;
    [SerializeField] private Animator animatorRef;
    private bool canBePickedUp = false;

    public void Start()
    {
        globalPickupAudioSource = CubePuzzleController.instance.audioSource;
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
        if (CubePuzzleController.instance.puzzleComplete || !canBePickedUp)
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
