using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject AboutUI;
    public GameObject BeforePlayUI;


    public void GoPlay()
    {
        AboutUI.SetActive(false);
        BeforePlayUI.SetActive(true);
    }

    public void GoAbout()
    {
        BeforePlayUI.SetActive(false);
        AboutUI.SetActive(true);
    }
    public void ExitGame()
    {
        SceneManager.LoadScene(-1);
    }
} 