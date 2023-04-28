using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public GameObject AboutUI;
     public GameObject ExitUI;

    public void GoPlay()
    {
        ExitUI.SetActive(false);
        AboutUI.SetActive(false);
    }   

    public void GoAbout()
    {
        ExitUI.SetActive(false);
        AboutUI.SetActive(true);
    }

    public void SureExit()
    {   
        AboutUI.SetActive(false);
        ExitUI.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
} 