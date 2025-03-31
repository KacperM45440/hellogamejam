using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEquipment : MonoBehaviour
{
    private List<string> heldObjectNames;
    private List<Sprite> heldObjectSprites;
    private GameObject slot0, slot1, slot2;
    private List<GameObject> slots = new();
    
    private void Start()
    {
        CreateItemSlots();
    }

    private void CreateItemSlots()
    {
        slots.Add(slot0);
        slots.Add(slot1);
        slots.Add(slot2);
    }

    public List<string> GetHeldObjects()
    {
        return heldObjectNames;
    }

    public void SetHeldObjectData(int slotIndex, string givenName, Sprite givenSprite)
    {
        heldObjectNames[slotIndex] = givenName;
        heldObjectSprites[slotIndex] = givenSprite;
    }

    public void AddObject(string givenName, Sprite givenSprite)
    {
        heldObjectNames.Add(givenName);
        heldObjectSprites.Add(givenSprite);
    }    

    public void ShowItems(int slotIndex)
    {
        slots[slotIndex].GetComponent<Image>().sprite = heldObjectSprites[slotIndex];
        
        Color c = slots[slotIndex].GetComponent<Image>().color;
        c.a = 1;
        slots[slotIndex].GetComponent<Image>().color = c;
    }

    public List<GameObject> GetItemSlots()
    {
        return slots;
    }

    public void SetItemSlotImage(int index, Sprite newImage, Color newColor)
    {
        slots[index].GetComponent<Image>().sprite = newImage;
        slots[index].GetComponent<Image>().color = newColor;
    }

    public bool DoesItemExist(string itemName) 
    {
        foreach (string name in heldObjectNames) 
        {
            if (name.Equals(itemName))
            {
                return true;
            }
        }

        return false;
    }

    public void DropItem(string itemName)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (GetHeldObjects()[i].Equals(itemName))
            {
                Color newColor = GetItemSlots()[i].GetComponent<Image>().color;
                newColor.a = 0;
                SetHeldObjectData(i, "", null);
                SetItemSlotImage(i, null, newColor);
                break;
            }
        }
    }
}    
