using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePickup : InteractableObject
{
    public Sprite pickupSprite;

    public override void Interact()
    {
        PlayerEquipment.Instance.heldObjects.Add(new Tuple<string, Sprite>(interactableName, pickupSprite));
        PlayerEquipment.Instance.ShowItems();
        Destroy(gameObject);
    }
}
