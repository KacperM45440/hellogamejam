using System.Linq;
using UnityEngine;

public class InteractablePickup : InteractableObject
{
    //Inspect level 4; this should either be a part of parent class or *un-quick-fixed* 
    //Also, InteractableCubePickup, line 13
    public AudioSource globalPickupAudioSource { get; private set; } 
    [SerializeField] private Sprite pickupSprite;
    private int maxInventorySlots = 3;

    public override void Interact()
    {
        if (InventoryIsFull())
        {
            return;
        }

        int emptySlotIndex = PlayerEquipmentRef.GetHeldObjects().FindIndex(name => name.Equals(""));
        if (emptySlotIndex != -1)
        {
            PlayerEquipmentRef.SetHeldObjectData(emptySlotIndex, InteractableName, pickupSprite);
        }
        else
        {
            PlayerEquipmentRef.AddObject(InteractableName, pickupSprite);
        }

        //Fix this with proper class hierarchy and/or assignments in inspector
        //globalPickupAudioSource.PlayOneShot(pickupAudio);
        PlayerEquipmentRef.ShowItems(emptySlotIndex);
        gameObject.SetActive(false);
    }

    private bool InventoryIsFull()
    {
        return PlayerEquipmentRef.GetHeldObjects().Count.Equals(maxInventorySlots) && PlayerEquipmentRef.GetHeldObjects().All(name => !name.Equals("") && PlayerEquipmentRef.GetHeldObjects().All(name => !name.Equals(null)));
    }
}



