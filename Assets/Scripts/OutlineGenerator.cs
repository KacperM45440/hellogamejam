using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class OutlineGenerator : MonoBehaviour
{
    [SerializeField] private Material transparentMat;
    [SerializeField] private AudioClip objectFound;
    private AudioSource audioRef;

    private void Start()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        audioRef = GetComponent<AudioSource>();
        audioRef.clip = objectFound;
    }

    public void GenerateOutline(GameObject copyReference, bool isAudible) 
    {
        // This probably shouldn't be generated on the fly, it's okay-ish for PC, but if we ship to mobile, constantly generating/destroying objects will get expensive (and by extension, noticeable in performance)
        // We should try to opt in for some sort of a loading screen in-between levels (plus when starting up game), load up outlines asynchronously per-object and then switch them on/off in game
        // With that said, i don't think it's necessary to unload outlines, because they will be dropped on level change, but it's something to keep in mind when we get to how we want to handle level change

        for (int i=0; i < copyReference.transform.childCount; i++)
        {
            if (copyReference.transform.GetChild(i).name.Equals("OUTLINECLONE")) // Rewrite this so that copies are searched for some class/attribute instead of comparing strings
            {
                return;
            }
        }

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
}
