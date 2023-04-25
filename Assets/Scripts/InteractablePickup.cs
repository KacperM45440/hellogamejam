using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePickup : InteractableObject
{
    public Sprite pickupSprite;
    private int maxInventorySlots = 3;

    public override void Interact()
    {
        if (PlayerEquipment.Instance.heldObjectNames.Count.Equals(maxInventorySlots) && PlayerEquipment.Instance.heldObjectNames.All(name => !name.Equals("") && PlayerEquipment.Instance.heldObjectNames.All(name => !name.Equals(null))))
        {
            Debug.Log("We're full, boss");
            return;
        }

        int emptySlotIndex = PlayerEquipment.Instance.heldObjectNames.FindIndex(name => name.Equals(""));

        Debug.Log(emptySlotIndex);

        if (emptySlotIndex != -1)
        {
            PlayerEquipment.Instance.heldObjectNames[emptySlotIndex] = interactableName;
            PlayerEquipment.Instance.heldObjectSprites[emptySlotIndex] = pickupSprite;
        }
        else
        {
            PlayerEquipment.Instance.heldObjectNames.Add(interactableName);
            PlayerEquipment.Instance.heldObjectSprites.Add(pickupSprite);
        }

        PlayerEquipment.Instance.ShowItems();
        Destroy(gameObject);
    }
}



