using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float speed = 200;
    public float jumpSpeed = 500;
    public float sidespin = 2500;
    public bool jumping = false;
    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = 0;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            x = -1;
        }
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            x = 1;
        }
        rb.AddForce(new Vector3(x, 0, 0) * speed * Time.deltaTime);
        if (!jumping)
        {
            rb.AddTorque(new Vector3(0, 0, -x) * sidespin * Time.deltaTime, ForceMode.Acceleration);
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                jumping = true;
                rb.AddForce(new Vector3(0, 1, 0) * jumpSpeed * Time.deltaTime, ForceMode.Impulse);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        jumping = false;
    }
}
