using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    [SerializeField] private PlayerMovement movementRef;
    [SerializeField] private PlayerEquipment equipmentRef;
    [SerializeField] private OutlineGenerator generatorRef;
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

    public OutlineGenerator GetOutlineGenerator()
    {
        return generatorRef;
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
