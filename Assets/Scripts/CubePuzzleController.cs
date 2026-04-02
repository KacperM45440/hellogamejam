using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubePuzzleController : MonoBehaviour
{
    [SerializeField] private PlayerReferences playerReferencesRef;
    [SerializeField] private PauseScript pauseRef;
    public AudioSource audioSource;
    public AudioClip windSound;
    public Transform platformsRef;

    public List<int> enabledPlatforms = new List<int>();
    public List<int> correctPlatforms = new List<int>();

    public bool puzzleComplete = false;
    private bool hasGenerated;
    [SerializeField] private Transform gate;
    [SerializeField] private Vector3 newGatePos;
    Vector3 gateTargetPos;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        gateTargetPos = gate.localPosition;
    }

    private void Update()
    {
        GenerateOutlines();
        gate.localPosition = Vector3.MoveTowards(gate.localPosition, gateTargetPos, 5f * Time.deltaTime);
    }

    private void GenerateOutlines()
    {
        if (playerReferencesRef.GetOutlineGenerator().name.Equals("Player") && !hasGenerated)
        {
            hasGenerated = true;
            for (int i=0; i < platformsRef.childCount; i++)
            {
                playerReferencesRef.GetOutlineGenerator().GenerateOutline(platformsRef.GetChild(i).gameObject, false);
            }
        }
    }

    [ContextMenu("TEST")]
    private void PuzzleCompleted()
    {
        Debug.Log("good job");
        puzzleComplete = true;
        gateTargetPos = newGatePos;
        audioSource.PlayOneShot(windSound);
        //PlayerReference.Instance.playerMovement.TurnToSkeleton();
        //StartCoroutine(shakeRef.Shake(2f, 1.5f));
    }

    public void EndGame() 
    {
        playerReferencesRef.GetPlayerMovement().DisableMovement();
        StartCoroutine(BackToMenu());
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

    public IEnumerator Completed()
    {
        yield return new WaitForSeconds(0.5f);
        pauseRef.ReturnToMenu();
    }

    public IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(1.5f);
        pauseRef.ReturnToMenu();
    }
}
