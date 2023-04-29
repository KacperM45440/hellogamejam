using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraRef;
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = cameraRef.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float scale = (duration / ((elapsed == 0) ? 1 : elapsed));
            float x = Random.Range(-1f, 1f) * magnitude * scale;
            float y = Random.Range(-1f, 1f) * magnitude * scale;
            float z = Random.Range(-1f, 1f) * magnitude * scale;

            //transform.localPosition = new Vector3 (x, y, z);
            cameraRef.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z + z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        cameraRef.localPosition = originalPos;
    }
}
