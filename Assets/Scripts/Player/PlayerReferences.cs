using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    [SerializeField] private PlayerMovement movementRef;
    [SerializeField] private PlayerEquipment equipmentRef;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Transform playerBodyTransform;
    
    public PlayerMovement GetPlayerMovement()
    {
        return movementRef;
    }

    public PlayerEquipment GetPlayerEquipment()
    {
        return equipmentRef;
    }

    public Animator GetPlayerAnimator()
    {
        return playerAnimator;
    }

    public Transform GetPlayerTransform()
    {
        return playerBodyTransform;
    }
}
