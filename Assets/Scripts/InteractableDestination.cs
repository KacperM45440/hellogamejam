using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableDestination : InteractableObject
{
    public string requiredItemName;
    public override void Interact()
    {
        if (PlayerEquipment.Instance.heldObjectNames[0].Equals(requiredItemName))
        {
            PlayerEquipment.Instance.heldObjectNames[0] = "";
            PlayerEquipment.Instance.heldObjectSprites[0] = null;
            PlayerEquipment.Instance.slot0.GetComponent<Image>().sprite = null;
        }
        if (PlayerEquipment.Instance.heldObjectNames[1].Equals(requiredItemName))
        {
            PlayerEquipment.Instance.heldObjectNames[1] = "";
            PlayerEquipment.Instance.heldObjectSprites[1] = null;
            PlayerEquipment.Instance.slot1.GetComponent<Image>().sprite = null;
        }
        if (PlayerEquipment.Instance.heldObjectNames[2].Equals(requiredItemName))
        {
            PlayerEquipment.Instance.heldObjectNames[2] = "";
            PlayerEquipment.Instance.heldObjectSprites[2] = null;
            PlayerEquipment.Instance.slot2.GetComponent<Image>().sprite = null;
        }
    }
}
