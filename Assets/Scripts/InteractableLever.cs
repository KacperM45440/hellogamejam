using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLever : InteractableObject
{
    [HideInInspector] public bool turnedOn = false;
    public override void Interact()
    {
        if (turnedOn)
        {
            turnedOn = false;
        }
        else
        {
            turnedOn = true;
        }

        Debug.Log(turnedOn);
    }
}
