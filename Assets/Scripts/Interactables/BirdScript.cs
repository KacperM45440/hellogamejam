using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public AudioSource birdSource;
    public AudioClip chirp1;
    public AudioClip chirp2;
    public AudioClip chirp3;
    public void Chirp()
    {
        int x = Random.Range(1, 4);
        switch (x)
        {
            case 1:
                birdSource.clip = chirp1;
                birdSource.Play();
                break;
            case 2:
                birdSource.clip = chirp2;
                birdSource.Play();
                break;
            case 3:
                birdSource.clip = chirp3;
                birdSource.Play();
                break;
        }
        Debug.Log(x);
    }
}
