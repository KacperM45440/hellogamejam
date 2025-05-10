using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// This class defines *how* a photo is taken
// Also see TakePhoto.cs
public class PhotoManager : MonoBehaviour
{
    [SerializeField] private Image mirrorImage;
    [SerializeField] private TakePhoto modelRef;
    [SerializeField] private CameraObjectsDetection detectionRef;
    [SerializeField] private GhostScript ghostRef;
    [SerializeField] private Vector2 photoResolution = new(340, 216);
    
    private Camera cameraRef;
    private string photosFolderPath = "Photos/";
    private string[] filePaths;
    private bool isGhostLevel = false;

    // After fixing WaitAndScreenshot() same changes should apply here.
    // Both blocks of code feature image loading - will this still be applicable in the memory loading version? Maybe this should be a reusable method?
    private void Start()
    {
        cameraRef = gameObject.GetComponent<Camera>();
        photosFolderPath = Application.streamingAssetsPath + "/Photos/";
        mirrorImage.gameObject.SetActive(false);

        // I don't exactly remember what the purpose for this try/catch block was.
        // Something to do with the fact that we always wanted to have a photo image ready for the UI?
        // If so, there's definitely a better way of delivering that feature
        // And even then it doesn't matter if the picture isn't loaded because we could have a spare black rectangle ready in the hierarchy

        try
        {
            filePaths = Directory.GetFiles(photosFolderPath, "defaultImage.png");
            byte[] pngBytes = File.ReadAllBytes(filePaths[0]);
            Texture2D newTex = new Texture2D(2, 2);
            newTex.LoadImage(pngBytes);

            Sprite newSprite = Sprite.Create(newTex, new Rect(0.0f, 0.0f, newTex.width, newTex.height), new Vector2(0.5f, 0.5f), 20);
            mirrorImage.sprite = newSprite;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public void TakeScreenshot()
    {
        int width = (int)photoResolution.x;
        int height = (int)photoResolution.y;
        cameraRef.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        cameraRef.targetTexture.filterMode = FilterMode.Point;

        StartCoroutine(WaitAndScreenshot());
        if (detectionRef.FindVisibleTargets() && isGhostLevel)
        {
            //ghostRef.RevealGhost();
        }
    }

    // URGENT: This method should be rewritten to save and load screenshots to/from memory, instead of doing it with the hard drive
    // This *should* be documented for future reference because fixing it is a mess
    private IEnumerator WaitAndScreenshot()
    {
        yield return new WaitForEndOfFrame(); // This should probably be a WaitUntil() instead
        RenderTexture renderTex = cameraRef.targetTexture;
        RenderTexture.active = renderTex;

        Texture2D resultTex = new Texture2D(renderTex.width, renderTex.height, TextureFormat.ARGB32, false); // What options other than "ARGB32" do we have?
        Rect rect = new Rect(0, 0, renderTex.width, renderTex.height);
        resultTex.ReadPixels(rect, 0, 0); // CopyTexture() should probably be faster. Also, is there no other way?

        byte[] byteArray = resultTex.EncodeToPNG(); // Probably no need to encode in memory saving scenario
        File.WriteAllBytes(photosFolderPath + "mirrorImage.png", byteArray); // SLOW AND BAD

        RenderTexture.ReleaseTemporary(renderTex); // Purpose? Is there no other way?
        cameraRef.targetTexture = null;

        yield return new WaitForSeconds(0.01f); // This should probably be a WaitUntil() instead

        filePaths = Directory.GetFiles(photosFolderPath, "mirrorImage.png"); // Probably no need to search assets in memory saving scenario
        byte[] pngBytes = File.ReadAllBytes(filePaths[0]); // SLOW AND BAD
        Texture2D newTex = new Texture2D(2, 2);
        newTex.LoadImage(pngBytes); // Probably no need after memory fix

        Sprite newSprite = Sprite.Create(newTex, new Rect(0.0f, 0.0f, newTex.width, newTex.height), new Vector2(0.5f, 0.5f), 20); // If this is going to be a part of UI, it should be an Image instead of a Sprite
        mirrorImage.sprite = newSprite;

        StartCoroutine(FadeIn());
        //modelRef.ScreenFade();
    }

    private IEnumerator FadeIn()
    {
        mirrorImage.gameObject.SetActive(true);
        Color c = mirrorImage.color; // More descriptive - what is "c", really? A color of which object?
        for (float alpha = 0f; alpha <= 100f; alpha += 5f)
        {
            c.a = alpha / 100;
            mirrorImage.color = c;
            yield return new WaitForSeconds(.05f);
        }
        modelRef.EnablePhotoCapture();
    }
}
