using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaperScript : MonoBehaviour
{
    [SerializeField] AudioSource paperSource;
    [SerializeField] AudioClip paperSound;
    private Animator animatorRef;
    private int childCount;
    private bool clickable = false;

    private void Start()
    {
        if (!transform.name.Equals("PaperHugeParent"))
        {
            return;
        }

        childCount = transform.childCount - 1;
        StartCoroutine(WaitStart());
    }
    private void Update()
    {
        DismissPapers();
    }
    public void DestroyMe()
    {
        Destroy(transform.parent.gameObject);
    }    

    private void DismissPapers()
    {
        if (!transform.name.Equals("PaperHugeParent") || !clickable)
        {
            return;
        }

        if(transform.childCount.Equals(0))
        {
            //Destroy(gameObject);
            return;
        }

        if (childCount < 0)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            animatorRef = transform.GetChild(childCount).GetChild(0).gameObject.GetComponent<Animator>();
            childCount--;
            paperSource = animatorRef.gameObject.GetComponent<AudioSource>();
            paperSource.Play();
            animatorRef.SetTrigger("Dismiss");
            if (childCount < 0)
            {
                StartCoroutine(WaitLoadLevel());
            }
        }
    }

    public void HideMe()
    {
        gameObject.SetActive(false);
    }

    IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(1.5f);
        clickable = true;
    }

    IEnumerator WaitLoadLevel()
    {
        yield return new WaitForSeconds(2.8f);
        SceneManager.LoadScene(1);
    }
}
