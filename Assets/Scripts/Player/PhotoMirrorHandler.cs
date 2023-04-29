using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PhotoMirrorHandler : MonoBehaviour
{
    [SerializeField] private Image mirrorImage;
    [SerializeField] private ModelTakePhoto modelRef;
    [SerializeField] private CameraObjectsDetection detectionRef;
    [SerializeField] private GhostScript ghostRef;
    private static PhotoMirrorHandler instance;
    private Camera cameraRef;
    private string photosFolderPath = "Photos/";
    private string[] filePaths;
    private float cogSpeedupMultiplier = 3f;
    private float cogsSpeedMax = 20;
    private float cogsCurrentSpeed = 1;
    private bool cogsSpeeding = false;
    private bool isGhostLevel = false;
    public Transform cogs;

    private void Awake()
    {
        instance = this;
        cameraRef = gameObject.GetComponent<Camera>();
        photosFolderPath = Application.streamingAssetsPath + "/Photos/";
        mirrorImage.gameObject.SetActive(false);

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
    IEnumerator WaitAndScreenshot()
    {
        yield return new WaitForEndOfFrame();
        RenderTexture renderTex = cameraRef.targetTexture;
        RenderTexture.active = renderTex;

        Texture2D resultTex = new Texture2D(renderTex.width, renderTex.height, TextureFormat.ARGB32, false);
        Rect rect = new Rect(0, 0, renderTex.width, renderTex.height);
        resultTex.ReadPixels(rect, 0, 0);

        byte[] byteArray = resultTex.EncodeToPNG();
        File.WriteAllBytes(photosFolderPath + "mirrorImage.png", byteArray);

        RenderTexture.ReleaseTemporary(renderTex);
        cameraRef.targetTexture = null;

        yield return new WaitForSeconds(0.01f);

        filePaths = Directory.GetFiles(photosFolderPath, "mirrorImage.png");
        byte[] pngBytes = File.ReadAllBytes(filePaths[0]);
        Texture2D newTex = new Texture2D(2, 2);
        newTex.LoadImage(pngBytes);

        Sprite newSprite = Sprite.Create(newTex, new Rect(0.0f, 0.0f, newTex.width, newTex.height), new Vector2(0.5f, 0.5f), 20);
        mirrorImage.sprite = newSprite;

        StartCoroutine(FadeIn());
        //modelRef.ScreenFade();
    }

    public void TakeScreenshot(int width, int height)
    {
        cameraRef.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        cameraRef.targetTexture.filterMode = FilterMode.Point;
        if(cogsCurrentSpeed < cogsSpeedMax)
        {
            cogsCurrentSpeed += 3 * cogSpeedupMultiplier;
        }
        if (!cogsSpeeding)
        {
            foreach (Transform child in cogs)
            {
                StartCoroutine(SpinCogs(child));
            }
        }
        StartCoroutine(WaitAndScreenshot());
        if (detectionRef.FindVisibleTargets() && isGhostLevel)
        {
            //bravo six, going dark
            //ghostRef.RevealGhost();
        }

    }

    IEnumerator SpinCogs(Transform cog)
    {
        cogsSpeeding = true;
        //for (float speed = 3f * cogSpeedupMultiplier; speed >= 1f; speed -= 0.05f)
        for (; cogsCurrentSpeed >= 1f; cogsCurrentSpeed -= 0.04f)
        {
            cog.gameObject.GetComponent<Animator>().speed = cogsCurrentSpeed;
            yield return new WaitForSeconds(.05f);
        }
        cogsSpeeding = false;
    }

    IEnumerator FadeIn()
    {
        mirrorImage.gameObject.SetActive(true);
        Color c = mirrorImage.color;
        for (float alpha = 0f; alpha <= 100f; alpha += 5f)
        {
            c.a = alpha/100;
            mirrorImage.color = c;
            yield return new WaitForSeconds(.05f);
        }
        modelRef.canTakePhoto = true;
    }
}
