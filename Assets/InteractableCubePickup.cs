using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCubePickup : InteractablePickup
{

    public GameObject parentPlatform;

    public override void Interact()
    {
        if (parentPlatform != null)
        {
            parentPlatform.GetComponent<InteractableCubePlatform>().PickedUpCube();
            parentPlatform = null;
        }
        base.Interact();
    }

    public void WaitUntilPickupable(float time)
    {
        canBeInteractedWith = false;
        StartCoroutine(Wait(time));
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        canBeInteractedWith = true;
        Debug.Log("pickupable");
    }
}
