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
        Debug.DrawRay(transform.position, (detectingCamera.transform.position - transform.position));

        if (GeometryUtility.TestPlanesAABB(cameraFrustumPlanes, bounds))
        {
            if (Physics.Raycast(transform.position, (detectingCamera.transform.position - transform.position), out RaycastHit hitInfo))
            {
                if (hitInfo.collider.CompareTag("Wall"))
                {
                    rendererRef.sharedMaterial.color = Color.red;
                }
                else
                {
                    rendererRef.sharedMaterial.color = Color.green;
                }    
            }
        }
        else
        {
            rendererRef.sharedMaterial.color = Color.red;
        }
    }
}
