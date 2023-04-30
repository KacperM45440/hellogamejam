using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] Audios;
    public AudioMixer mixer;
    public Slider sfxSlider, musicSlider;
    const string MIXER_SFX = "SFXVolume";
    const string MIXER_MUSIC = "MusicVolume";
    public void Awake()
    {

        sfxSlider.onValueChanged.AddListener(SetSFXValume);
        musicSlider.onValueChanged.AddListener(SetMusicValume);
    }

    private void SetMusicValume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);

    }
    private void SetSFXValume(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value)*20);

    }
}
