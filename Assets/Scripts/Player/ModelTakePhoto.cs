using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelTakePhoto : MonoBehaviour
{
    [SerializeField] private GameObject cubeToFade;
    [SerializeField] private PhotoMirrorHandler handlerRef;
    private Animator animatorRef;
    Material m;
    private PlayerMovement playerMovement;
    [SerializeField] private Light spotLight;
    [SerializeField] private bool useSpotLight = false;
    [SerializeField] private AudioClip takePhotoClip;
    private AudioSource audioSource;

    public bool canTakePhoto = true;
    private void Start()
    {
        animatorRef = GetComponent<Animator>();
        m = cubeToFade.GetComponent<MeshRenderer>().material;
        playerMovement = FindObjectOfType<PlayerMovement>();
        spotLight.gameObject.SetActive(useSpotLight);
        spotLight.intensity = 0f;
        audioSource = GetComponent<AudioSource>();
    }
    public void Update()
    {
        if (!canTakePhoto)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            canTakePhoto = false;
            ScreenFade();
            animatorRef.SetTrigger("TakePhoto");
        }
    }
    public void TakePhoto()
    {
        handlerRef.TakeScreenshot(340, 216);
    }
    public void ScreenFade()
    {
        playerMovement.DoAction("TakePhoto", 1.5f);
        StartCoroutine(FadeScreen());
    }

    IEnumerator FadeScreen()
    {
        audioSource.clip = takePhotoClip;
        audioSource.Play();
        yield return new WaitForSeconds(0.5f);

        playerMovement.freezeMovement = true;
        Color c = m.color;
        spotLight.intensity = 20;

        for (float alpha = 100f; alpha >= 0f; alpha -= 5f)
        {
            c.a = alpha / 100;
            spotLight.intensity = alpha / 100;
            m.color = c;
            yield return new WaitForSeconds(0.02f);
        }

        playerMovement.freezeMovement = false;
        spotLight.intensity = 0f;
    }
}
