using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MirrorCameraInteractableObject : MonoBehaviour
{

    [SerializeField] private UnityEvent eventObject;

    public void DoEvent(float time) {
        Invoke("DoEvent", time);
    }

    public void DoEvent() {
        eventObject.Invoke();
    }
}
