using Newtonsoft.Json.Bson;
using UnityEngine;

public class PlayerInteractCollider : MonoBehaviour
{
    [HideInInspector] public InteractableObject interactableObject;

    private void OnTriggerEnter(Collider collision)
    {
        AssignObject(collision);
    }

    private void OnTriggerExit(Collider collision)
    {
        ClearObject(collision);
    }

    private void Update()
    {
        InteractInRange(); //Should be moved to New Input System
    }

    private void AssignObject(Collider collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            interactableObject = collision.gameObject.GetComponent<InteractableObject>();
        }
    }

    private void ClearObject(Collider collision)
    {
        if (interactableObject.gameObject == collision.gameObject)
        {
            interactableObject = null;
        }
    }

    private void InteractInRange()
    {
        if (interactableObject != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactableObject.Interact();
            }
        }
    }
}
