using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SijayRisingScript : MonoBehaviour
{
    Transform transform;
    public float risingSpeed = (float)0.01;
    // Start is called before the first frame update
    void Start()
    {
        transform = this.GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        float y = transform.position.y + risingSpeed;
        transform.SetPositionAndRotation(new Vector3(transform.position.x, y, transform.position.z), new Quaternion(0, 0, 0, 0));
    }
}
