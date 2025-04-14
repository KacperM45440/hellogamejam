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

        int emptySlotIndex = GetPlayerEquipment().GetHeldObjects().FindIndex(name => name.Equals(""));
        if (emptySlotIndex != -1)
        {
            GetPlayerEquipment().SetHeldObjectData(emptySlotIndex, InteractableName, pickupSprite);
        }
        else
        {
            GetPlayerEquipment().AddObject(InteractableName, pickupSprite);
        }

        //Fix this with proper class hierarchy and/or assignments in inspector
        //globalPickupAudioSource.PlayOneShot(pickupAudio);
        GetPlayerEquipment().ShowItems(emptySlotIndex);
        gameObject.SetActive(false);
    }

    private bool InventoryIsFull()
    {
        return GetPlayerEquipment().GetHeldObjects().Count.Equals(maxInventorySlots) && GetPlayerEquipment().GetHeldObjects().All(name => !name.Equals("") && GetPlayerEquipment().GetHeldObjects().All(name => !name.Equals(null)));
    }
}



