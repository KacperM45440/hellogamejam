using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraObjectsDetection : MonoBehaviour
{
    [SerializeField] private PlayerReferences playerReferencesRef;
    [SerializeField] [Range(0, 360)] private float viewAngle;
    [SerializeField] private float viewRadius;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private List<Transform> visibleTargets = new();

    public bool FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            Vector3 dirToTargetAngle = (new Vector3(target.position.x, transform.position.y, target.position.z) - transform.position).normalized;

            if (IsInFront(dirToTargetAngle))
            {
                continue;
            }

            if (IsBehindWall(dirToTarget, target))
            {
                continue;
            }

            visibleTargets.Add(target);
            AffectedByCamera(target);

            if (IsBirdcage(target))
            {
                BirdScript.Instance.GetComponent<BirdScript>().Chirp();
                break;
            }

            if (IsGhost(target))
            {
                break;
            }

            playerReferencesRef.GetOutlineGenerator().GenerateOutline(target.gameObject, true);
        }

        if (visibleTargets.Count == 0)
        {
            return false;
        }

        return true;
    }

    public List<Transform> GetVisibleTargets()
    {
        return visibleTargets;
    }

    public float GetViewAngle()
    {
        return viewAngle;
    }

    public float GetViewRadius()
    {
        return viewRadius;
    }

    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private bool IsInFront(Vector3 dirToTargetAngle)
    {
        return Vector3.Angle(transform.forward, dirToTargetAngle) >= viewAngle * 0.5f;
    }

    // This could possibly be done with a trigger instead of a raycast
    private bool IsBehindWall(Vector3 dirToTarget, Transform target)
    {
        float dstToTarget = Vector3.Distance(transform.position, target.position);
        return Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask);
    }

    private void AffectedByCamera(Transform target)
    {
        if (target.TryGetComponent(out MirrorCameraInteractableObject interactableObject))
        {
            interactableObject.DoEvent();
        }
    }

    private bool IsBirdcage(Transform target)
    {
        return target.name.Equals("Birdcage");
    }

    private bool IsGhost(Transform target)
    {
        return target.gameObject.layer.Equals(LayerMask.NameToLayer("GhostHidden"));
    }
}
