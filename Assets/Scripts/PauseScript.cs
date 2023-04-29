using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private PlayerMovement movementRef;
    [SerializeField] private ModelTakePhoto photoRef;
    public GameObject overlayRef;
    public RawImage gameMask;
    public Image mirrorMask;
    private bool isPaused;


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
                return;
            }

            isPaused = true;
            movementRef.freezeMovement = true;
            photoRef.enabled = false;
            overlayRef.SetActive(true);
        }
    }

    public void ReturnToMenu()
    {
        //czysto technicznie loadscene nie powinien byc przypiety do FadeScreens, ale i tak nic innego tego nie uzyje
        StartCoroutine(FadeScreens());
    }

    public void Resume()
    {
        isPaused = false;
        overlayRef.SetActive(false);
        movementRef.freezeMovement = false;
        photoRef.enabled = true;
    }

    IEnumerator FadeScreens()
    {
        overlayRef.SetActive(false);

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
        SceneManager.LoadScene(0);
    }
}
