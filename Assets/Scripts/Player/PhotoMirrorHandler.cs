using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PhotoMirrorHandler : MonoBehaviour
{
    [SerializeField] private Image mirrorImage;
    [SerializeField] private ModelTakePhoto modelRef;
    private static PhotoMirrorHandler instance;
    private Camera cameraRef;
    private string photosFolderPath = "Photos/";
    private string[] filePaths;
    private bool canTakePhoto = true;
    private void Awake()
    {
        instance = this;
        cameraRef = gameObject.GetComponent<Camera>();
        photosFolderPath = Application.streamingAssetsPath + "/Photos/";

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
        modelRef.TakePhoto();
    }

    public void TakeScreenshot(int width, int height)
    {
        if (!canTakePhoto)
        {
            return;
        }

        canTakePhoto = false;
        cameraRef.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        StartCoroutine(WaitAndScreenshot());
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeScreenshot(500, 500);
        }
    }

    IEnumerator FadeIn()
    {
        Color c = mirrorImage.color;
        for (float alpha = 0f; alpha <= 100f; alpha += 5f)
        {
            c.a = alpha/100;
            mirrorImage.color = c;
            yield return new WaitForSeconds(.05f);
        }
        canTakePhoto = true;
    }
}
