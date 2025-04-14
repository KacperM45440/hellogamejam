using System.Collections;
using UnityEngine;

public class InteractableLever : InteractableObject
{
    [SerializeField] private Animator animatorRef;
    [SerializeField] private int leverID;
    private bool animationIsPlaying = false;
    private bool turnedOn = false;
    
    public override void Interact()// powinien by� nieinteractable dop�ki trwa animacja, chyba �e wywo�ane przez "ResetItself()", wtedy ma czeka� a� animacja si� sko�czy i od razu interaktowa� znowu
    {
        if (!animationIsPlaying)
        {
            if (turnedOn)
            {
                animatorRef.SetTrigger("Move");
            }
            else
            {
                animatorRef.SetTrigger("Move");
            }
            animationIsPlaying = true;
            LeverCounter.instance.PullLever(leverID);
        }

        OutlineGenerator.Instance.GenerateOutline(transform.GetChild(0).transform.gameObject, false);
        OutlineGenerator.Instance.GenerateOutline(transform.GetChild(1).transform.gameObject, false);
    }

    public void AnimationFinish()
    {
        animationIsPlaying = false;
        turnedOn = !animatorRef.GetBool("TurnedOn");
        animatorRef.SetBool("TurnedOn", turnedOn);
    }

    public void ResetItself()
    {
        StartCoroutine(WaitAndReset());
    }

    private IEnumerator WaitAndReset()
    {
        yield return new WaitWhile(() => animationIsPlaying);

        if (turnedOn)
        {
            yield return new WaitForSeconds(Random.Range(0.0f, 0.2f));
            animationIsPlaying = true;
            turnedOn = false;
            animatorRef.SetTrigger("Move");
            yield return new WaitForSeconds(1.5f);
            PlaySound();
        }
    }
}
