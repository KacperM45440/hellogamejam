using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DoAfterTime : MonoBehaviour
{
    [SerializeField] private float secondsToWait;
    [SerializeField] private UnityEvent stuffToDo;
    private void Start()
    {
        StartCoroutine(DoAfterEvent());
    }
    private IEnumerator DoAfterEvent()
    {
        yield return new WaitForSeconds(secondsToWait);
        stuffToDo.Invoke();
    }
}
