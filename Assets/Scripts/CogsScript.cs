using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogsScript : MonoBehaviour
{
    private List<Animator> cogsAnimators;
    private float cogsSpeedupMultiplier = 3f;
    private float cogsSpeedMax = 20;
    private float cogsCurrentSpeed = 1;
    private bool cogsSpeeding = false;

    private void Start()
    {
        InitializeReferences();
    }

    public void SpinCogs()
    {
        CogSpinLogic();
    }

    private void InitializeReferences()
    {
        foreach (Transform cog in transform)
        {
            cogsAnimators.Add(cog.gameObject.GetComponent<Animator>());
        }
    }

    private void CogSpinLogic()
    {
        if (cogsCurrentSpeed < cogsSpeedMax)
        {
            cogsCurrentSpeed += 3 * cogsSpeedupMultiplier;
        }

        if (cogsSpeeding)
        {
            return;
        }

        foreach (Animator cogAnimator in cogsAnimators)
        {
            StartCoroutine(SpinCogs(cogAnimator));
        }
    }

    private IEnumerator SpinCogs(Animator cogAnimator)
    {
        cogsSpeeding = true;
        for (; cogsCurrentSpeed >= 1f; cogsCurrentSpeed -= 0.04f)
        {
            cogAnimator.speed = cogsCurrentSpeed;
            yield return new WaitForSeconds(.05f); // This should probably be a WaitUntil() instead
        }

        cogsSpeeding = false;
    }
}
