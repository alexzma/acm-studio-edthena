using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitTeleport : MonoBehaviour
{
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    public Vector2 spawn;

    private void Update()
    {
        if (transform.position.x > maxX || transform.position.y > maxY 
            || transform.position.x < minX || transform.position.y < minY)
        {
            transform.position = spawn;
        }
    }
}
