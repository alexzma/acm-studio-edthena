using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerAlex : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform transform;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        transform = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(rb.position.x, rb.position.y, transform.position.z);
    }
}