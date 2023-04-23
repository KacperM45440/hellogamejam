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

            //cam.transform.LookAt(Vector3.Scale(realativePos, new Vector3(-1, 1, 1)));

            Vector3 relativeRot = transform.InverseTransformDirection(playerCamera.transform.forward);
            relativeRot = Vector3.Scale(relativeRot, new Vector3(1, 1, -1));

            cam.transform.forward = transform.TransformDirection(relativeRot);
            //cam.transform.forward = relativeRot;
            //Vector3 cameraPortalOffset = new Vector3(0f, 0f, playerCamera.transform.eulerAngles.z);
            //cam.transform.localEulerAngles = cameraPortalOffset;

        }
    }

    void InitPortal()
    {
        cam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        renderer.material.SetTexture("_MirrorTexture", cam.targetTexture);
        cam.targetTexture.Release();  
    }
}
