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
            if (PlayerEquipmentRef.GetHeldObjects()[i].Equals(requiredItemName))
            {
                Color newColor = PlayerEquipmentRef.GetItemSlots()[i].GetComponent<Image>().color;
                newColor.a = 0;
                PlayerEquipmentRef.SetHeldObjectData(i, "", null);
                PlayerEquipmentRef.SetItemSlotImage(i, null, newColor);
            }
        }
    }
}
