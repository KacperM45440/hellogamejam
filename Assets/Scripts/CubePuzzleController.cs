using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubePuzzleController : MonoBehaviour
{
    public static CubePuzzleController instance;
    public AudioSource audioSource;
    public Transform platformsRef;

    public List<int> enabledPlatforms = new List<int>();
    public List<int> correctPlatforms = new List<int>();

    public bool puzzleComplete = false;
    private bool hasGenerated;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        GenerateOutlines();
    }

    private void GenerateOutlines()
    {
        if (OutlineGenerator.Instance.name.Equals("Player") && !hasGenerated)
        {
            hasGenerated = true;
            for (int i=0; i < platformsRef.childCount; i++)
            {
                OutlineGenerator.Instance.GenerateOutline(platformsRef.GetChild(i).gameObject, false);
            }
        }
    }
    private void PuzzleCompleted()
    {
        Debug.Log("good job");
        puzzleComplete = true;
        PlayerReference.Instance.playerMovement.TurnToSkeleton();
        //StartCoroutine(shakeRef.Shake(2f, 1.5f));
    }

    public void PlatformTurnOn(int platform)
    {
        if (!enabledPlatforms.Contains(platform))
        {
            enabledPlatforms.Add(platform);
            if (IsSame(enabledPlatforms, correctPlatforms))
            {
                PuzzleCompleted();
            }
        }
    }

    public void PlatformTurnOff(int platform)
    {
        enabledPlatforms.Remove(platform);
    }

    bool IsSame(List<int> a, List<int> b)
    {
        if (a.Count != b.Count)
        {
            return false;
        }

        List<int> aSort = a.ToList();
        aSort.Sort();
        List<int> bSort = b.ToList();
        bSort.Sort();

        for (int i = 0; i < aSort.Count; i++)
        {
            if (aSort[i] != bSort[i])
            {
                return false;
            }
        }

        return true;
    }
}
