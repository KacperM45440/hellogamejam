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
    }

    public void ShowItems()
    {
        slot0.GetComponent<Image>().sprite = heldObjectSprites[0];
        slot1.GetComponent<Image>().sprite = heldObjectSprites[1];
        slot2.GetComponent<Image>().sprite = heldObjectSprites[2];
    }    
}
