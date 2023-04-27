using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineGenerator : MonoBehaviour
{
    private static OutlineGenerator _instance;
    public static OutlineGenerator Instance { get { return _instance; } }
    public Material transparentMat;

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
    public void GenerateOutline(GameObject copyReference)
    {
        GameObject clone = Instantiate(copyReference, copyReference.transform);
        clone.transform.localPosition = new Vector3(0, 0, 0);
        clone.GetComponent<Renderer>().material = transparentMat;
        clone.layer = LayerMask.NameToLayer("OutlineLayer");
        Destroy(clone.GetComponent<Rigidbody>());
        Destroy(clone.GetComponent<Collider>());

        Outline cloneScript = clone.AddComponent<Outline>();
        cloneScript.OutlineWidth = 20f;
        cloneScript.OutlineColor = Color.red;
    }
}
