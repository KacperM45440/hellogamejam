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
            if (GetPlayerEquipment().GetHeldObjects()[i].Equals(requiredItemName))
            {
                Color newColor = GetPlayerEquipment().GetItemSlots()[i].GetComponent<Image>().color;
                newColor.a = 0;
                GetPlayerEquipment().SetHeldObjectData(i, "", null);
                GetPlayerEquipment().SetItemSlotImage(i, null, newColor);
            }
        }
    }
}
