using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject overlayRef;
    [SerializeField] private PlayerMovement movementRef;
    [SerializeField] private ModelTakePhoto photoRef;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            movementRef.enabled = false;
            photoRef.enabled = false;
            overlayRef.SetActive(true);
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0); 
    }

    public void Resume()
    {
        overlayRef.SetActive(false);
        movementRef.enabled = true;
        photoRef.enabled = true;
    }
}
