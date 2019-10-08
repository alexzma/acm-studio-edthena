using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackground : MonoBehaviour
{
    public float interval;

    Camera cam;
    float t = 0;
    int colorSwitch = 1;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Time.time > t)
        {
            t = Time.time + interval;
            if (colorSwitch == 1)
            {
                cam.backgroundColor = Color.black;
                colorSwitch = 0;
            }
            else
            {
                cam.backgroundColor = new Color(0.9f, 0.9f, 0.9f, 1);
                colorSwitch = 1;
            }
        }
    }
}
