using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Animator caseAnimatorRef;

    [SerializeField] private GameObject AboutUI;
    [SerializeField] private GameObject SureExitUI;
    [SerializeField] private GameObject InsideGameUI;

    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button backButton;

    private void Awake()
    {
        RemovePlayerFromGame();
    }

    void Update()
    {
        ShowCursor();
    }

    private void RemovePlayerFromGame()
    {
        if (PlayerReference.Instance != null)
        {
            Destroy(PlayerReference.Instance.transform.parent.gameObject);
            caseAnimatorRef.SetTrigger("MenuAnimClose");
        }
        else
        {
            //If menu scene is the first scene loaded (game launch), ignore error
            Debug.LogError("Menu - Player instance not found.");
        }
    }

    private void ShowCursor()
    {
        Cursor.visible = true;
    }

    public void PlayButton()
    {
        playButton.interactable= false;
        optionsButton.interactable = false;
        exitButton.interactable = false;

        SureExitUI.SetActive(false);
        InsideGameUI.SetActive(true);
        AboutUI.SetActive(false);   
    }

    public void OptionsButton()
    {
        playButton.interactable = false;
        optionsButton.interactable = false;
        exitButton.interactable = false;

        SureExitUI.SetActive(false);
        AboutUI.SetActive(true);
    }

    public void ExitButton()
    {
        playButton.interactable = false;
        optionsButton.interactable = false;
        exitButton.interactable = false;

        SureExitUI.SetActive(true);
        InsideGameUI.SetActive(false);
        AboutUI.SetActive(false);
    }

    public void BackButton()
    {
        playButton.interactable = true;
        optionsButton.interactable = true;
        exitButton.interactable = true;
        backButton.interactable = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
} 
