using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(CameraObjectsDetection))]
public class CameraObjectsDetectionEditor :Editor
{
    private void OnSceneGUI()
    {
        CameraObjectsDetection detection = (CameraObjectsDetection)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(detection.transform.position, Vector3.up, Vector3.forward, 360, detection.viewRadius);
        Vector3 viewAngleA = detection.DirFromAngle(-detection.viewAngle / 2, false);
        Vector3 viewAngleB = detection.DirFromAngle(detection.viewAngle / 2, false);

        Handles.DrawLine(detection.transform.position, detection.transform.position + viewAngleA * detection.viewRadius);
        Handles.DrawLine(detection.transform.position, detection.transform.position + viewAngleB * detection.viewRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in detection.visibleTargets) {
            Handles.DrawLine(detection.transform.position, visibleTarget.position);
        }
    }
}
