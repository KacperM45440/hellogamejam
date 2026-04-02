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

        int emptySlotIndex = PlayerReferencesRef.GetPlayerEquipment().GetHeldObjects().FindIndex(name => name.Equals(""));
        if (emptySlotIndex != -1)
        {
            PlayerReferencesRef.GetPlayerEquipment().SetHeldObjectData(emptySlotIndex, GetInteractableName(), pickupSprite);
        }
        else
        {
            PlayerReferencesRef.GetPlayerEquipment().AddObject(GetInteractableName(), pickupSprite);
        }

        //Fix this with proper class hierarchy and/or assignments in inspector
        //globalPickupAudioSource.PlayOneShot(pickupAudio);
        PlayerReferencesRef.GetPlayerEquipment().ShowItems(emptySlotIndex);
        gameObject.SetActive(false);
    }

    private bool InventoryIsFull()
    {
        return PlayerReferencesRef.GetPlayerEquipment().GetHeldObjects().Count.Equals(maxInventorySlots) && PlayerReferencesRef.GetPlayerEquipment().GetHeldObjects().All(name => !name.Equals("") && PlayerReferencesRef.GetPlayerEquipment().GetHeldObjects().All(name => !name.Equals(null)));
    }
}



