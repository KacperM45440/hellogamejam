using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractCollider : MonoBehaviour
{
    [HideInInspector] public InteractableObject interactableObject;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            Debug.Log("found sth interactable");
            interactableObject = collision.gameObject.GetComponent<InteractableObject>();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(interactableObject.gameObject == collision.gameObject)
        {
            interactableObject = null;
        }
    }

    private void Update()
    {
        if (interactableObject != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactableObject.Interact();
                Debug.Log("I interacted with " + interactableObject.interactableName);
            }
        }
    }
}
