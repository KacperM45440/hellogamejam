using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEquipment : MonoBehaviour
{
    private static PlayerEquipment _instance;
    public static PlayerEquipment Instance { get { return _instance; } }
    public List<Tuple<string, Sprite>> heldObjects = new();
    [SerializeField] private GameObject slot1;
    [SerializeField] private GameObject slot2;

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
        Debug.Log("Moje itemki to:" + heldObjects[0]);
        slot1.GetComponent<Image>().sprite = heldObjects[0].Item2;
    }    
}
