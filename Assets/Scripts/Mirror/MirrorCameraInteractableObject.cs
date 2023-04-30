using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MirrorCameraInteractableObject : MonoBehaviour
{

    [SerializeField] private UnityEvent eventObject;
    [SerializeField] private float time;

    public void DoEvent() {
        Invoke("StartEvent", time);
    }

    public void StartEvent() {
        eventObject.Invoke();
    }
}
