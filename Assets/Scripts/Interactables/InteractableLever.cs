using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLever : InteractableObject
{
    public int myLeverId;

    [HideInInspector] public bool turnedOn = false;

    private AudioSource audioRef;
    private Animator animatorRef;
    private bool animationPlaying = false;

    private void Start()
    {
        animatorRef = GetComponent<Animator>();
        audioRef = GetComponent<AudioSource>();
    }

    public override void Interact()// powinien byæ nieinteractable dopóki trwa animacja, chyba ¿e wywo³ane przez "ResetItself()", wtedy ma czekaæ a¿ animacja siê skoñczy i od razu interaktowaæ znowu
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
            yield return new WaitForSeconds(Random.Range(0.1f, 0.7f));
            animationPlaying = true;
            turnedOn = false;
            animatorRef.SetTrigger("Move");
            yield return new WaitForSeconds(1.6f);
            audioRef.Play();
        }
    }
}
