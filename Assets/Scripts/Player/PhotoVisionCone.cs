using Unity.VisualScripting;
using UnityEngine;

public class PhotoVisionCone : MonoBehaviour
{
    [SerializeField] private Material visionConeMaterial;
    [SerializeField] private float visionRange;
    [SerializeField] private float visionAngle;
    [SerializeField] private LayerMask visionObstructingLayer;
    [SerializeField] private int visionConeResolution = 120;
    private Mesh visionConeMesh = new();
    private MeshFilter meshFilter;

    private void Start()
    {
        transform.AddComponent<MeshRenderer>().material = visionConeMaterial;
        meshFilter = transform.AddComponent<MeshFilter>();
        visionAngle *= Mathf.Deg2Rad;
    }

    private void Update()
    {
        DrawVisionCone();
    }

    private void DrawVisionCone()
    {
        int[] triangles = new int[(visionConeResolution - 1) * 3];
        Vector3[] Vertices = new Vector3[visionConeResolution + 1];
        Vertices[0] = Vector3.zero;
        float Currentangle = -visionAngle / 2;
        float angleIcrement = visionAngle / (visionConeResolution - 1);
        float Sine;
        float Cosine;

        for (int i = 0; i < visionConeResolution; i++)
        {
            Sine = Mathf.Sin(Currentangle);
            Cosine = Mathf.Cos(Currentangle);
            Vector3 RaycastDirection = (transform.forward * Cosine) + (transform.right * Sine);
            Vector3 VertForward = (Vector3.forward * Cosine) + (Vector3.right * Sine);
            
            if (Physics.Raycast(transform.position, RaycastDirection, out RaycastHit hit, visionRange, visionObstructingLayer))
            {
                Vertices[i + 1] = VertForward * hit.distance;
            }
            else
            {
                Vertices[i + 1] = VertForward * visionRange;
            }

            Currentangle += angleIcrement;
        }
        
        for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
        {
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j + 2;
        }
        
        visionConeMesh.Clear();
        visionConeMesh.vertices = Vertices;
        visionConeMesh.triangles = triangles;
        meshFilter.mesh = visionConeMesh;
    }

}

