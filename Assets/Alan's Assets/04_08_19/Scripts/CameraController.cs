﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float cameraOffset;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //cameraOffset = transform.position.x - player.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        cameraOffset = transform.position.x - player.transform.position.x;
    }


}
