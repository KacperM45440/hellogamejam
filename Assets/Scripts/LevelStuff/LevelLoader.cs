using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    private PlayerMovement playerMovement;
    [SerializeField] private Image displayFade;
    private Transform cameraSystem;
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        cameraSystem = GameObject.FindGameObjectWithTag("CameraSystem").transform;
    }

    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NextLevel") {
            if (other.TryGetComponent<LevelLoaderInfo>(out LevelLoaderInfo info)) {
                StartCoroutine(LoadLevel(info.newxLevelPlayerPosition));
            }
        }
    }
    
    IEnumerator LoadLevel(Vector3 newPos) {

        playerMovement.freezeMovement = true;
        Color color = displayFade.color;
        color.a = 0f;
        float elapsedTime = 0f;
        float duration = 1f;
        while (elapsedTime < duration)
        {
            color.a = (elapsedTime / duration);
            displayFade.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        transform.position = newPos;
        cameraSystem.position = transform.position;
        playerMovement.freezeMovement = false;
        while (elapsedTime < duration)
        {
            color.a = 1f - (elapsedTime / duration);
            displayFade.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
