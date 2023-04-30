using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperScript : MonoBehaviour
{
    //[SerializeField] private ActionKeeper keeperRef;
    private Animator animatorRef;
    private int childCount;
    private void Start()
    {
        if (!transform.name.Equals("PaperHugeParent"))
        {
            return;
        }

        childCount = transform.childCount - 1;
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
        if (!transform.name.Equals("PaperHugeParent"))
        {
            return;
        }

        if(transform.childCount.Equals(0))
        {
            Destroy(gameObject);
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
            animatorRef.SetTrigger("Dismiss");
        }
    }
}
