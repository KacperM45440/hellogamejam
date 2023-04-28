using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Animator caseAnimatorRef;
    public GameObject AboutUI;
    public GameObject BeforePlayUI;


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
