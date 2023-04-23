using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelTakePhoto : MonoBehaviour
{
    [SerializeField] private Image imageToFade;

    public void TakePhoto()
    {
        StartCoroutine(FadeScreen());
    }
    IEnumerator FadeScreen()
    { 
        Color c = imageToFade.color;
        for (float alpha = 100f; alpha >= 0f; alpha -= 5f)
        {
            c.a = alpha/100;
            imageToFade.color = c;
            yield return new WaitForSeconds(.05f);
        }
    }    
}
