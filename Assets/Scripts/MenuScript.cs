using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Animator caseAnimatorRef;
    public GameObject AboutUI;
    public GameObject SureExitUI;
    public GameObject InsideGameUI;

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

    //Dzia³a git
    public void GoPlay()
    {
        InsideGameUI.SetActive(true);
        AboutUI.SetActive(false);
        SureExitUI.SetActive(false);
        StartCoroutine(WaitStart());
    }

    //Dzia³a git
    public void GoAbout()
    {
        SureExitUI.SetActive(false);
        AboutUI.SetActive(true);
    }

    public void SureExit()
    {
        SureExitUI.SetActive(true);
        AboutUI.SetActive(false);
        InsideGameUI.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
} 
