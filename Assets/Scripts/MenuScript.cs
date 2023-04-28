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
        
        StartCoroutine(WaitStart());
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

    IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
} 
