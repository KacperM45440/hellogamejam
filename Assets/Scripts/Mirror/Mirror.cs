using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private Camera cam;

    private Camera playerCamera;

    void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("PlayerMirrorCamera").GetComponent<Camera>();
        InitPortal();
    }

    void Update()
    {
        if (playerCamera)
        {
            cam.projectionMatrix = playerCamera.projectionMatrix;
            Vector3 realativePos = transform.InverseTransformPoint(playerCamera.transform.position);
            cam.transform.position = transform.TransformPoint(Vector3.Scale(realativePos, new Vector3(1, 1, -1)));
            Vector3 relativeRot = transform.InverseTransformDirection(playerCamera.transform.forward);
            cam.transform.rotation =  Quaternion.Inverse(playerCamera.transform.rotation);
            cam.transform.localEulerAngles = new Vector3(cam.transform.localEulerAngles.x, cam.transform.localEulerAngles.y + 180f, cam.transform.localEulerAngles.z);
            //cam.nearClipPlane = Vector3.Distance(cam.transform.position, transform.position);
        }
    }

    void InitPortal()
    {
        cam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        renderer.material.SetTexture("_MirrorTexture", cam.targetTexture);
        cam.targetTexture.Release();  
    }
}
