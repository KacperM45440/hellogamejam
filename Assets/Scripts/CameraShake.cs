using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Camera cameraRef;
    private Transform cameraTransform;

    private void Start()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        cameraTransform = cameraRef.transform;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = cameraTransform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float scale = (duration / ((elapsed == 0) ? 0.1f : elapsed));
            float x = Random.Range(-1f, 1f) * magnitude * scale;
            float y = Random.Range(-1f, 1f) * magnitude * scale;
            float z = Random.Range(-1f, 1f) * magnitude * scale;

            cameraTransform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z + z);
            elapsed += Time.deltaTime;

            yield return null;
        }

        cameraTransform.localPosition = originalPos;
    }
}
