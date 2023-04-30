using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterReference : MonoBehaviour
{
    private static LetterReference _instance;
    public static LetterReference Instance { get { return _instance; } }

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

