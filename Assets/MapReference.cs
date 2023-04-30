using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapReference : MonoBehaviour
{
    private static MapReference _instance;
    public static MapReference Instance { get { return _instance; } }

    public Animator animatorRef;

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
}

