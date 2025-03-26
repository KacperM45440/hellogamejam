using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private float cameraSpeed;
    
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, cameraTarget.position, cameraSpeed * Time.deltaTime);
    }
}
