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

    public bool canTakePhoto = true;
    private void Start()
    {
        animatorRef = GetComponent<Animator>();
        m = cubeToFade.GetComponent<MeshRenderer>().material;
        playerMovement = FindObjectOfType<PlayerMovement>();

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
        handlerRef.TakeScreenshot(340, 216);
    }
    public void ScreenFade()
    {
        StartCoroutine(FadeScreen());
    }

    IEnumerator FadeScreen()
    {
        playerMovement.freezMovement = true;
        Color c = m.color;
        for (float alpha = 100f; alpha >= 0f; alpha -= 5f)
        {
            c.a = alpha / 100;
            m.color = c;
            yield return new WaitForSeconds(.02f);
        }
        playerMovement.freezMovement = false;

    }
}
