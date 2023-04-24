using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLever : InteractableObject
{
    private MeshRenderer rendererRef; //zamiast tego syfu z materia�ami testowymi zrobi� animator kt�ry porusza d�wigni�
    public Material materialOn; //do usuni�cia
    public Material materialOff; //do usuni�cia
    public int myLeverId;

    [HideInInspector] public bool turnedOn = false;

    private bool automaticReset = false;

    private void Start()
    {
        rendererRef = GetComponent<MeshRenderer>(); //do usuni�cia
    }

    public override void Interact()// powinien by� nieinteractable dop�ki trwa animacja, chyba �e wywo�ane przez "ResetItself()", wtedy ma czeka� a� animacja si� sko�czy i od razu interaktowa� znowu
    {
        if (turnedOn)
        {
            rendererRef.material = materialOff; //do usuni�cia
            turnedOn = false;
        }
        else
        {
            rendererRef.material = materialOn; //do usuni�cia
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
