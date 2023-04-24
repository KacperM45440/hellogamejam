using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLever : InteractableObject
{
    private MeshRenderer rendererRef; //zamiast tego syfu z materia³ami testowymi zrobiæ animator który porusza dŸwigni¹
    public Material materialOn; //do usuniêcia
    public Material materialOff; //do usuniêcia
    public int myLeverId;

    [HideInInspector] public bool turnedOn = false;

    private bool automaticReset = false;

    private void Start()
    {
        rendererRef = GetComponent<MeshRenderer>(); //do usuniêcia
    }

    public override void Interact()// powinien byæ nieinteractable dopóki trwa animacja, chyba ¿e wywo³ane przez "ResetItself()", wtedy ma czekaæ a¿ animacja siê skoñczy i od razu interaktowaæ znowu
    {
        if (turnedOn)
        {
            rendererRef.material = materialOff; //do usuniêcia
            turnedOn = false;
        }
        else
        {
            rendererRef.material = materialOn; //do usuniêcia
            turnedOn = true;
        }
        if (!automaticReset)
        {
            LeverCounter.instance.PullLever(myLeverId);
        }
        else
        {
            automaticReset = false;
        }


        Debug.Log(turnedOn + " for lever " + myLeverId);
    }

    public void ResetItself()
    {
        if (turnedOn)
        {
            automaticReset = true;
            Interact();
        }
    }
}
