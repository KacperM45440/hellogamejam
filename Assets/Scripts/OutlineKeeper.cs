using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineKeeper : MonoBehaviour
{
    private Outline outlineRef;

    private void Start()
    {
        outlineRef = transform.GetComponent<Outline>();
    }
    void Update()
    {
        TrackDistance();
        Rotate();
    }
    private void TrackDistance()
    {
        float currentDistance = Mathf.Abs(Vector3.Distance(PlayerReference.Instance.transform.position, transform.position));
        float distanceInverted = (1 / currentDistance) * 25;
        float distanceSmoothed = Mathf.Round(distanceInverted * 10.0f) * 0.1f;


        if (distanceSmoothed >= 15f)
        {
            distanceSmoothed = 20f;
        }

        if (distanceSmoothed <= 4f)
        {
            distanceSmoothed = 0f;
        }

        outlineRef.OutlineWidth = distanceSmoothed;
        Debug.Log(distanceSmoothed);
    }
    private void Rotate()
    {
        transform.Rotate(0, 120 * Time.deltaTime, 0);
    }
}
