using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float speed = 100;
    public float jumpSpeed = 250;
    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = 0;
        rb.AddForce(new Vector3(x - z, 0, 0) * speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            y = 1;
        }
        rb.AddForce(new Vector3(0, y, 0) * jumpSpeed * Time.deltaTime, ForceMode.Impulse);
    }
}
