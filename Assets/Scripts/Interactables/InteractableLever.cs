using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLever : InteractableObject
{
    public int myLeverId;

    [HideInInspector] public bool turnedOn = false;

    private Animator animatorRef;
    private bool animationPlaying = false;

    private void Start()
    {
        animatorRef = GetComponent<Animator>();
    }

    public override void Interact()// powinien by� nieinteractable dop�ki trwa animacja, chyba �e wywo�ane przez "ResetItself()", wtedy ma czeka� a� animacja si� sko�czy i od razu interaktowa� znowu
    {
        Debug.Log("is On: " + turnedOn + " is playing: " + animationPlaying);
        if (!animationPlaying)
        {
            if (turnedOn)
            {
                animatorRef.SetTrigger("Move");
            }
            else
            {
                animatorRef.SetTrigger("Move");
            }
            animationPlaying = true;
            LeverCounter.instance.PullLever(myLeverId);
        }
    }

    public void AnimationFinish()
    {
        animationPlaying = false;
        turnedOn = !animatorRef.GetBool("TurnedOn");
        animatorRef.SetBool("TurnedOn", turnedOn);
    }

    public void ResetItself()
    {
        StartCoroutine(WaitAndReset());
    }

    IEnumerator WaitAndReset()
    {
        yield return new WaitWhile(() => animationPlaying);

        Debug.Log("my id " + myLeverId + "am turned " + turnedOn);
        if (turnedOn)
        {
            animationPlaying = true;
            turnedOn = false;
            animatorRef.SetTrigger("Move");
        }
    }
}