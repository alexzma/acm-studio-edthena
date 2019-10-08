using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDir : MonoBehaviour
{
    public Vector2 dir;
    public float speed;

    private void Update()
    {
        transform.position = (Vector2)transform.position + dir * speed;
    }
}
