using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineGenerator : MonoBehaviour
{
    private static OutlineGenerator _instance;
    public static OutlineGenerator Instance { get { return _instance; } }
    public Material transparentMat;
    private bool hasOutline;
    public AudioClip objectFound;
    public AudioSource audioRef;

    private void Start()
    {
        audioRef.clip = objectFound;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void GenerateOutline(GameObject copyReference, bool isAudible)
    {
        for (int i=0; i < copyReference.transform.childCount; i++)
        {
            if (copyReference.transform.GetChild(i).name.Equals("OUTLINECLONE"))
            {
                hasOutline = true;
            }
        }

        if (!hasOutline)
        {
            GameObject clone = Instantiate(copyReference, copyReference.transform);
            clone.name = "OUTLINECLONE";
            clone.transform.localPosition = new Vector3(0, 0, 0);
            clone.transform.localScale = new Vector3(1, 1, 1);
            clone.transform.rotation = copyReference.transform.rotation;
            clone.GetComponent<Renderer>().material = transparentMat;
            clone.layer = LayerMask.NameToLayer("OutlineLayer");
            Destroy(clone.GetComponent<Rigidbody>());
            Destroy(clone.GetComponent<Collider>());

            Outline cloneScript = clone.AddComponent<Outline>();
            cloneScript.OutlineColor = Color.white;
            clone.AddComponent<OutlineKeeper>();

            if (isAudible)
            {
                audioRef.Play();
            }
        }

        hasOutline = false;
    }
}
