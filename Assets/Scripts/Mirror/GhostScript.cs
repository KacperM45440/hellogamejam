using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    [SerializeField] private CameraObjectsDetection detectionRef;
    public void RevealGhost()
    {
        for (int i=0; i < detectionRef.visibleTargets.Count; i++)
        {
            if (detectionRef.visibleTargets[i].gameObject.layer.Equals(LayerMask.NameToLayer("GhostHidden")))
            {
                detectionRef.visibleTargets[i].gameObject.layer = LayerMask.NameToLayer("GhostVisible");
                foreach (Transform child in detectionRef.visibleTargets[i].gameObject.transform)
                {
                    child.gameObject.layer = LayerMask.NameToLayer("GhostVisible"); 
                }
            }
        } 
    }
}
