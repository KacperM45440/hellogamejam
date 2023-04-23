using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoMirrorHandler : MonoBehaviour
{
    [SerializeField] private Image mirrorImage;

    private static PhotoMirrorHandler instance;
    private Camera cameraRef;
    private void Awake()
    {
        instance = this;
        cameraRef = gameObject.GetComponent<Camera>();
    }

    IEnumerator WaitAndScreenshot()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("bonj33");
        RenderTexture renderTex = cameraRef.targetTexture;
        RenderTexture.active = renderTex;
        Debug.Log("bonj");

        Texture2D resultTex = new Texture2D(renderTex.width, renderTex.height, TextureFormat.ARGB32, false);
        Rect rect = new Rect(0, 0, renderTex.width, renderTex.height);
        resultTex.ReadPixels(rect, 0, 0);

        byte[] byteArray = resultTex.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + "/Resources/Photos/" + "mirrorImage.png", byteArray);

        Debug.Log("eeee");
        RenderTexture.ReleaseTemporary(renderTex);
        cameraRef.targetTexture = null;

        yield return new WaitForSeconds(1);
        Texture2D newTex = Resources.Load<Texture2D>("Photos/mirrorImage");
        Sprite newSprite = Sprite.Create(newTex, new Rect(0.0f, 0.0f, newTex.width, newTex.height), new Vector2(0.5f, 0.5f), 20);
        mirrorImage.sprite = newSprite;
    }

    public void TakeScreenshot(int width, int height)
    {
        cameraRef.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        Debug.Log("bonj2");
        StartCoroutine(WaitAndScreenshot());
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("bonj1");
            TakeScreenshot(500, 500);
        }
    }
}
