using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristal : MonoBehaviour
{

    private MeshRenderer renderer;

    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        ChangeColor();
    }

    public void ChangeColor() {
        float a = renderer.material.color.a;
        Color color = Random.ColorHSV(0f, 1f, 0f, 1f, 0.5f, 1f);
        color.a = a;
        renderer.material.color = color;
    }
}
