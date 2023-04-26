using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsSystem : MonoBehaviour
{
    [SerializeField] private FootStepsSystem otherFoot;
    [SerializeField] private AudioClip[] footStepClips;
    private AudioSource audioSource;
    [HideInInspector] public int lastIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "InteractZone") {
            PlayStepAudio();
        }
    }

    private void PlayStepAudio() {
        int index = Random.Range(0, footStepClips.Length - 1);
        while (index == lastIndex || index == otherFoot.lastIndex) {
            index = Random.Range(0, footStepClips.Length - 1);
        }
        lastIndex = index;
        AudioClip clip = footStepClips[index];
        audioSource.clip = clip;
        audioSource.Play();
    }
}
