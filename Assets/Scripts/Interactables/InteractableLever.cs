using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CameraShake))]
public class InteractableLever : InteractableObject
{
    [SerializeField] private LeverCounter leverCounterRef;
    [SerializeField] private int leverID;
    private OutlineGenerator outlineGeneratorRef;
    private Animator animatorRef;
    private bool animationIsPlaying = false;
    private bool turnedOn = false;

    private void Start()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        animatorRef = GetComponent<Animator>();
        outlineGeneratorRef = PlayerReferencesRef.GetOutlineGenerator();
    }

    public override void Interact()// powinien byæ nieinteractable dopóki trwa animacja, chyba ¿e wywo³ane przez "ResetItself()", wtedy ma czekaæ a¿ animacja siê skoñczy i od razu interaktowaæ znowu
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
            leverCounterRef.PullLever(leverID);
        }

        outlineGeneratorRef.GenerateOutline(transform.GetChild(0).transform.gameObject, false);
        outlineGeneratorRef.GenerateOutline(transform.GetChild(1).transform.gameObject, false);
    }

    //public void AnimationFinish()
    //{
    //    animationIsPlaying = false;
    //    turnedOn = !animatorRef.GetBool("TurnedOn");
    //    animatorRef.SetBool("TurnedOn", turnedOn);
    //}

    public void ResetItself()
    {
        StartCoroutine(WaitAndReset());
    }

    private IEnumerator WaitAndReset()
    {
        yield return new WaitWhile(() => animationIsPlaying);

        if (!turnedOn)
        {
            yield break;
        }

        yield return new WaitForSeconds(Random.Range(0.0f, 0.2f));
        animationIsPlaying = true;
        turnedOn = false;
        animatorRef.SetTrigger("Move");
        yield return new WaitForSeconds(1.5f);
        PlaySound();
    }
}
