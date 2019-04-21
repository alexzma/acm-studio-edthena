using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewaysScript : MonoBehaviour
{
    Transform transform;
    public float sidewaysSpeed = (float)0.01;
    // Start is called before the first frame update
    void Start()
    {
        transform = this.GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x + sidewaysSpeed;
        transform.SetPositionAndRotation(new Vector3(x, transform.position.y, transform.position.z), Quaternion.Euler(0,0,90));
    }
}
