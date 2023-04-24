using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePickup : InteractableObject
{
    public Sprite pickupSprite;
    public override void Interact()
    {
        //forgive me father for i have sinned

        if (!PlayerEquipment.Instance.heldObjectNames[1].Equals(null) && !PlayerEquipment.Instance.heldObjectNames[1].Equals("") && !PlayerEquipment.Instance.heldObjectNames[0].Equals(""))
        {
            Debug.Log("We're full, boss");
            return;
        }
        if (!PlayerEquipment.Instance.heldObjectNames.ElementAt(0).Equals(null) && !PlayerEquipment.Instance.heldObjectNames[0].Equals(""))
        {
            PlayerEquipment.Instance.heldObjectNames[1] = interactableName;
            PlayerEquipment.Instance.heldObjectSprites[1] = pickupSprite;
        }
        else
        {
            PlayerEquipment.Instance.heldObjectNames[0] = interactableName;
            PlayerEquipment.Instance.heldObjectSprites[0] = pickupSprite;
        }

        PlayerEquipment.Instance.ShowItems();
        Destroy(gameObject);
    }
}
