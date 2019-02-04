﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float speed = 100;
    public float jumpSpeed = 250;
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
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = 0;
        rb.AddForce(new Vector3(x - z, 0, 0) * speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            y = 1;
            jumping = true;
        }
        rb.AddForce(new Vector3(0, y, 0) * jumpSpeed * Time.deltaTime, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        jumping = false;
    }
}
