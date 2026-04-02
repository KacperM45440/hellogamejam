using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private PlayerMovement movementRef;
    [SerializeField] private TakePhoto photoRef;
    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private RawImage gameMask;
    [SerializeField] private Image mirrorMask;
    [SerializeField] private string menuSceneName = "Menu";
    private bool isPaused;

    //Should be moved to the New Input System whenever, for the sake of multi-platform
    private void Update()
    {
        CheckEscapeKey();
    }

    private void CheckEscapeKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused())
            {
                ResumeGame();
                return;
            }

            PauseGame();
        }
    }

    public bool IsGamePaused()
    {
        return isPaused == true;
    }

    private void PauseGame()
    {
        isPaused = true;
        movementRef.DisableMovement();
        photoRef.enabled = false;
        pauseOverlay.SetActive(true);
    }

    private void ResumeGame()
    {
        isPaused = false;
        pauseOverlay.SetActive(false);
        movementRef.EnableMovement();
        photoRef.enabled = true;
        Cursor.visible = false;
    }

    public void ReturnToMenu()
    {
        StartCoroutine(FadeScreens());
    }

    private IEnumerator FadeScreens()
    {
        pauseOverlay.SetActive(false);

        Color c1 = gameMask.color;
        Color c2 = mirrorMask.color;
        for (float alpha = 100f; alpha >= 0f; alpha -= 5f)
        {
            c1.a = alpha / 100;
            c2.a = alpha / 100;
            gameMask.color = c1;
            mirrorMask.color = c2;
            yield return new WaitForSeconds(.05f);
        }

        isPaused = false;
        SceneManager.LoadScene(menuSceneName); //Move this line away someplace else for sake of FadeScreens() reusability?
    }
}
