using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverCounter : MonoBehaviour
{
    public static LeverCounter instance;
    public List<InteractableLever> levers = new List<InteractableLever>();

    [SerializeField] private GameObject gateOpen;
    [SerializeField] private GameObject gateClosed;
    [SerializeField] private List<int> correctSequence = new List<int>();

    private int currentNumber = 0;

    private void Awake()
    {
        instance = this;
    }

    public void PullLever(int leverNumber)
    {
        if(currentNumber > correctSequence.Count)
        {
            return;
        }
        currentNumber++;
        if(leverNumber == correctSequence[currentNumber - 1])
        {
            if(currentNumber >= correctSequence.Count)
            {
                CorrectCombination();
            }
        }
        else
        {
            currentNumber = 0;
            IncorrectCombination();
        }
    }

    private void CorrectCombination()
    {
        Debug.Log("you did a good job!");
        gateOpen.SetActive(true);
        gateClosed.SetActive(false);
        gateOpen.GetComponent<AudioSource>().Play();
    }

    public void IncorrectCombination()
    {
        Debug.Log("you fucked up!");
        foreach(InteractableLever l in levers)
        {
            l.ResetItself();
        }
    }
}
