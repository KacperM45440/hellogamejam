using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Animator caseAnimatorRef;
    public GameObject AboutUI;
    public GameObject SureExitUI;
    public GameObject InsideGameUI;
    public Button playButton;
    public Button optionsButton;
    public Button exitButton;
    public Button backButton;

    private void Awake()
    {
        try
        {
            Destroy(PlayerReference.Instance.transform.parent.gameObject);
            caseAnimatorRef.SetTrigger("MenuAnimClose");
        }
        catch
        {
            Debug.Log("nie ma go");
        }
    }

    void Update()
    {
        if (!Cursor.visible) { Cursor.visible = true; }
    }

    //Dzia³a git
    public void GoPlay()
    {
        playButton.interactable= false;
        optionsButton.interactable = false;
        exitButton.interactable = false;
        InsideGameUI.SetActive(true);
        AboutUI.SetActive(false);
        SureExitUI.SetActive(false);
        //StartCoroutine(WaitStart());
    }

    //Dzia³a git
    public void GoAbout()
    {
        playButton.interactable = false;
        optionsButton.interactable = false;
        exitButton.interactable = false;
        SureExitUI.SetActive(false);
        AboutUI.SetActive(true);
    }

    public void SureExit()
    {
        playButton.interactable = false;
        optionsButton.interactable = false;
        exitButton.interactable = false;
        SureExitUI.SetActive(true);
        AboutUI.SetActive(false);
        InsideGameUI.SetActive(false);
    }

    public void BackButton()
    {
        backButton.interactable = true;
        playButton.interactable = true;
        optionsButton.interactable = true;
        exitButton.interactable = true;

    }

    //tu jest git
    public void ExitGame()
    {
        exitButton.interactable = false;
        backButton.interactable = false;
        Application.Quit();
    }

    public IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
} 
