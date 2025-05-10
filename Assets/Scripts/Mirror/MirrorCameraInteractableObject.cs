using UnityEngine;
using UnityEngine.Events;

public class MirrorCameraInteractableObject : MonoBehaviour
{
    [SerializeField] private UnityEvent eventObject;
    [SerializeField] private float time;

    public void DoEvent() 
    {
        Invoke(nameof(StartEvent), time);
    }

    public void StartEvent()
    {
        eventObject.Invoke();
    }
}
