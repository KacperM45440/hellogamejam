using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableDestination : InteractableObject
{
    public string requiredItemName;
    private int maxInventorySlots = 3;
    public override void Interact()
    {
        for (int i = 0; i < maxInventorySlots; i++)
        {
            if (PlayerEquipment.Instance.heldObjectNames[i].Equals(requiredItemName))
            {
                PlayerEquipment.Instance.heldObjectNames[i] = "";
                PlayerEquipment.Instance.heldObjectSprites[i] = null;
                PlayerEquipment.Instance.slots[i].GetComponent<Image>().sprite = null;

                Color c = PlayerEquipment.Instance.slots[i].GetComponent<Image>().color;
                c.a = 0;
                PlayerEquipment.Instance.slots[i].GetComponent<Image>().color = c;
            }
        }
    }
}
