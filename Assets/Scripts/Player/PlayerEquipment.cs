using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEquipment : MonoBehaviour
{
    private static PlayerEquipment _instance;
    public static PlayerEquipment Instance { get { return _instance; } }
    public List<string> heldObjectNames;
    public List<Sprite> heldObjectSprites;
    public GameObject slot0;
    public GameObject slot1;
    public GameObject slot2;

    public List<GameObject> slots = new();
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        slots.Add(slot0);
        slots.Add(slot1);
        slots.Add(slot2);
    }

    public void ShowItems(int slotIndex)
    {
        slots[slotIndex].GetComponent<Image>().sprite = heldObjectSprites[slotIndex];
        
        Color c = slots[slotIndex].GetComponent<Image>().color;
        c.a = 1;
        slots[slotIndex].GetComponent<Image>().color = c;
    }
}    
