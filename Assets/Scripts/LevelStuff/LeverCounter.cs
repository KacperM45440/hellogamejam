using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraShake))]
public class LeverCounter : MonoBehaviour
{
    [SerializeField] private GameObject gateOpen;
    [SerializeField] private GameObject gateClosed;
    [SerializeField] private List<InteractableLever> levers = new();
    [SerializeField] private List<int> correctSequence = new();

    private CameraShake cameraShakeRef;
    private int currentLever = 0;

    private void Start()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        cameraShakeRef = GetComponent<CameraShake>();
    }

    public void PullLever(int leverNumber)
    {
        if(currentLever > correctSequence.Count)
        {
            return;
        }
        
        currentLever++;

        if (IsNextLever(leverNumber))
        {
            CorrectCombination();
        }
        else
        {
            currentLever = 0;
            IncorrectCombination();
        }
    }

    private void CorrectCombination()// This should be rewritten to a variable UnityEvent for the purpose of lever script reusability
    {
        StartCoroutine(cameraShakeRef.Shake(2f, 1.5f)); 
        gateOpen.SetActive(true);
        gateClosed.SetActive(false);
        gateOpen.GetComponent<AudioSource>().Play();
    }

    private void IncorrectCombination()
    {
        foreach(InteractableLever lever in levers)
        {
            lever.ResetItself();
        }
    }

    private bool IsNextLever(int leverNumber)
    {
        return leverNumber == correctSequence[currentLever - 1] && currentLever >= correctSequence.Count;
    }
}
