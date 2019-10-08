using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + Vector3.back*5, 0.04f);
        }
    }
}