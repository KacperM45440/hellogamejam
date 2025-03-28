using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //Move entire class to DontDestroyOnLoad(), and add music handling on top of settings?
    //Remove audio source from top of player prefab when appropriate
    //Initialize manager in menu scene, make it a singleton

    [SerializeField] private AudioSource[] audioList;
    [SerializeField] private AudioMixer mixerRef;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    private const string MIXER_SFX = "SFXVolume";
    private const string MIXER_MUSIC = "MusicVolume";
    
    private void Start()
    {
        sfxSlider.onValueChanged.AddListener(SetSFXValume);
        musicSlider.onValueChanged.AddListener(SetMusicValume);
    }

    private void SetMusicValume(float value)
    {
        mixerRef.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }

    private void SetSFXValume(float value)
    {
        mixerRef.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }
}
