using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePaper : InteractableObject
{
    public Animator animatorRef;
    private bool letterOpened = false;
    public override void Interact()
    {
        if (letterOpened)
        { 
            return; 
        }
        animatorRef.gameObject.SetActive(true);
        animatorRef.SetTrigger("Appear");
        letterOpened = true;
    }

    public new void Update()
    {
        base.Update();

        try
        {
            animatorRef = LetterReference.Instance.animatorRef;
        }
        catch
        {

        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!letterOpened)
            {
                return;
            }
            animatorRef.SetTrigger("DismissND");
            letterOpened = false;
        }
    }
}
