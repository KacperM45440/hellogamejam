using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraObjectsDetection))]
public class CameraObjectsDetectionEditor : Editor
{
    // What did we check with this?
    private void OnSceneGUI()
    {
        CameraObjectsDetection detection = (CameraObjectsDetection)target;
        
        Handles.color = Color.white;
        Handles.DrawWireArc(detection.transform.position, Vector3.up, Vector3.forward, 360, detection.GetViewRadius());
        Vector3 viewAngleA = detection.DirectionFromAngle(-detection.GetViewAngle() / 2, false);
        Vector3 viewAngleB = detection.DirectionFromAngle(detection.GetViewAngle() / 2, false);

        Handles.DrawLine(detection.transform.position, detection.transform.position + viewAngleA * detection.GetViewRadius());
        Handles.DrawLine(detection.transform.position, detection.transform.position + viewAngleB * detection.GetViewRadius());

        Handles.color = Color.red;
        
        foreach (Transform visibleTarget in detection.GetVisibleTargets()) 
        {
            Handles.DrawLine(detection.transform.position, visibleTarget.position);
        }
    }
}
