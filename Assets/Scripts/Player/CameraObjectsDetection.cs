using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObjectsDetection : MonoBehaviour
{

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public List<Transform> visibleTargets = new List<Transform>();


    public bool FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);


        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2f)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                Debug.Log(target.name);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    if (target.TryGetComponent<MirrorCameraInteractableObject>(out MirrorCameraInteractableObject interactableObject))
                    {
                        interactableObject.DoEvent(1.5f);
                    }
                    else if(target.TryGetComponent<InteractablePickup>(out InteractablePickup interactable))
                    {
                        OutlineGenerator.Instance.GenerateOutline(target.gameObject);
                    }
                    
                }
            }
        }

        if (visibleTargets.Count.Equals(0))
        {
            return false;
        }

        return true;
    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
