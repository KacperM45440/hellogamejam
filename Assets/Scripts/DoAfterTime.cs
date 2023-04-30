using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoAfterTime : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private UnityEvent afterEvent;
    void Start()
    {
        StartCoroutine(DoAfterEvent());
    }
    IEnumerator DoAfterEvent()
    {
        yield return new WaitForSeconds(time);
        afterEvent.Invoke();
    }
}
