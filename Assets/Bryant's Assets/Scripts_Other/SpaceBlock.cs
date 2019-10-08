using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBlock : MonoBehaviour
{
    float LerpL, LerpR;
    static float t = 0.0f;

    private void Awake()
    {
        LerpL = transform.position.x - 5;
        LerpR = transform.position.x + 5;
    }

    void Update()
    {
        t += 1f * Time.deltaTime;
        transform.position = new Vector2(Mathf.Lerp(LerpL, LerpR, t), transform.position.y);
        if (t > 1.0f)
        {
            float temp = LerpR;
            LerpR = LerpL;
            LerpL = temp;
            t = 0.0f;
        }
    }
}
