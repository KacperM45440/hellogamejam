using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObjects : MonoBehaviour
{
    [SerializeField] private Camera detectingCamera;
    private MeshRenderer rendererRef;
    private Plane[] cameraFrustumPlanes;
    private Collider objectCollider;

    private void Start()
    {
        rendererRef = GetComponent<MeshRenderer>();
        objectCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        Bounds bounds = objectCollider.bounds;
        cameraFrustumPlanes = GeometryUtility.CalculateFrustumPlanes(detectingCamera);
        if(GeometryUtility.TestPlanesAABB(cameraFrustumPlanes, bounds))
        {
            rendererRef.sharedMaterial.color = Color.green;
        }
        else
        {
            rendererRef.sharedMaterial.color = Color.red;
        }
    }
}
