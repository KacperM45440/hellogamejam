using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelTakePhoto : MonoBehaviour
{
    [SerializeField] private GameObject cubeToFade;
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
        Material m = cubeToFade.GetComponent<Renderer>().material;
        Color c = m.color;
        for (float alpha = 100f; alpha >= 0f; alpha -= 5f)
        {
            c.a = alpha / 100;
            m.color = c;
            yield return new WaitForSeconds(.05f);
        }
    }
}
