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
            if (PlayerReferencesRef.GetPlayerEquipment().GetHeldObjects()[i].Equals(requiredItemName))
            {
                Color newColor = PlayerReferencesRef.GetPlayerEquipment().GetItemSlots()[i].GetComponent<Image>().color;
                newColor.a = 0;
                PlayerReferencesRef.GetPlayerEquipment().SetHeldObjectData(i, "", null);
                PlayerReferencesRef.GetPlayerEquipment().SetItemSlotImage(i, null, newColor);
            }
        }
    }
}
