using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelTakePhoto : MonoBehaviour
{
    [SerializeField] private Image imageToFade;
    [SerializeField] private PhotoMirrorHandler handlerRef;
    private Animator animatorRef;

    public bool canTakePhoto = true;
    private void Start()
    {
        animatorRef = GetComponent<Animator>();
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
            animatorRef.SetTrigger("TakePhoto");
        }
    }
    public void TakePhoto()
    {
        handlerRef.TakeScreenshot(500, 500);
    }
    public void ScreenFade()
    {
        StartCoroutine(FadeScreen());
    }

    IEnumerator FadeScreen()
    {
        Color c = imageToFade.color;
        for (float alpha = 100f; alpha >= 0f; alpha -= 5f)
        {
            c.a = alpha / 100;
            imageToFade.color = c;
            yield return new WaitForSeconds(.05f);
        }
    }
}
