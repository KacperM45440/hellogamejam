using System.Collections;
using UnityEngine;

// This class defines *what* happens when a photo is taken
// Also see PhotoManager.cs
public class TakePhoto : MonoBehaviour
{
    [SerializeField] private PlayerReferences playerReferencesRef;
    [SerializeField] private PhotoManager photoManagerRef;
    [SerializeField] private GameObject mirrorFadeCone;
    [SerializeField] private AudioSource mirrorAudioSource;
    [SerializeField] private AudioClip takePhotoSFX;
    [SerializeField] private Light mirrorSpotlight;
    [SerializeField] private CogsScript cogsRef;
    [SerializeField] private bool useSpotLight = false;
    [SerializeField] private bool canTakePhoto = true;

    private PlayerMovement playerMovement;
    private Animator playerAnimator;
    private Material coneMaterial;
    
    private void Start()
    {
        InitializeReferences();
    }
    public void Update()
    {
        PhotoLogic();
    }

    private void InitializeReferences()
    {
        playerAnimator = playerReferencesRef.GetPlayerAnimator();
        playerMovement = playerReferencesRef.GetPlayerMovement();
        coneMaterial = mirrorFadeCone.GetComponent<MeshRenderer>().material;
        mirrorSpotlight.gameObject.SetActive(useSpotLight);
        mirrorSpotlight.intensity = 0f;
    }
    
    //Both Photo() and PhotoLogic() need better method names
    public void Photo()
    {
        cogsRef.SpinCogs();
        photoManagerRef.TakeScreenshot();
    }

    //Should be moved to New Input System
    private void PhotoLogic()
    {
        if (!canTakePhoto)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            DisablePhotoCapture();
            playerMovement.StopAndPlayAnimation("TakePhoto", 1.5f); //Which one...
            ScreenFade();
            playerAnimator.SetTrigger("TakePhoto"); //...is the animation call being unused here?
        }
    }

    public void EnablePhotoCapture()
    {
        canTakePhoto = true;
    }

    public void DisablePhotoCapture()
    {
        canTakePhoto = false;
    }

    public void ScreenFade()
    {
        StartCoroutine(FadeScreen());
    }

    private IEnumerator FadeScreen()
    {
        mirrorAudioSource.clip = takePhotoSFX;
        mirrorAudioSource.pitch = Random.Range(0.96f, 1.05f);
        mirrorAudioSource.Play();
        yield return new WaitForSeconds(0.5f);

        playerMovement.DisableMovement();
        Color c = coneMaterial.color;
        mirrorSpotlight.intensity = 20;

        for (float alpha = 100f; alpha >= 0f; alpha -= 5f)
        {
            c.a = alpha / 100;
            mirrorSpotlight.intensity = alpha / 100;
            coneMaterial.color = c;
            yield return new WaitForSeconds(0.02f);
        }

        playerMovement.EnableMovement();
        mirrorSpotlight.intensity = 0f;
    }
}

