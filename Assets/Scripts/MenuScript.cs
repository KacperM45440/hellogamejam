using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public GameObject AboutUI;
     public GameObject ExitUI;
    public GameObject InsideSuitcaseUI;
    public AudioSource SkrzypEffect;

    public void GoPlay()
    {
        ExitUI.SetActive(false);
        AboutUI.SetActive(false);
        SkrzypEffect.Play();
    }   

    public void GoAbout()
    {
        ExitUI.SetActive(false);
        AboutUI.SetActive(true);
        SkrzypEffect.Play();
    }

    public void SureExit()
    {   
        AboutUI.SetActive(false);
        ExitUI.SetActive(true);
        SkrzypEffect.Play();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
} 