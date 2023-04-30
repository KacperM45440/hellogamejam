using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    private PlayerMovement playerMovement;
    [SerializeField] private Image displayFade;
    [SerializeField] private AudioClip scream;
    private Transform cameraSystem;
    private CharacterController controller;
    private AudioSource audioSource;
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
        cameraSystem = GameObject.FindGameObjectWithTag("CameraSystem").transform;
    }

    void Update()
    {
        
    }

    void Start()
    {
        /*controller.enabled = false;
        Transform start = GameObject.FindGameObjectWithTag("START").transform;
        transform.position = start.position;
        cameraSystem.position = transform.position;
        controller.enabled = true;*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NextLevel") {
            if (other.TryGetComponent<LevelLoaderInfo>(out LevelLoaderInfo info)) {
                StartCoroutine(LoadLevel(info.newxLevelPlayerPosition, info.loadEvent));
            }
        }
    }
    
    IEnumerator LoadLevel(Vector3 newPos, UnityEvent e) {

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
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            audioSource.PlayOneShot(scream);
            yield return new WaitForSeconds(7.5f);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("NEW POS: " + newPos);
        controller.enabled = false;
        transform.position = newPos;
        controller.enabled = true;
        cameraSystem.position = transform.position;
        playerMovement.freezeMovement = false;
        e.Invoke();
        
        while (elapsedTime < duration)
        {
            color.a = 1f - (elapsedTime / duration);
            displayFade.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

}
