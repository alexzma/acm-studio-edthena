using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControllerAlex : MonoBehaviour
{
    //public SpriteControllerVelocity controller;
    public SpriteControllerAlex controller;

    //public RespawnController spawnControl;
    private float h_move = 0f;
    private bool m_jump = false;

    private bool paused;

    [Range (0,100)][SerializeField] private float moveSpeed = 50f;

    // Update is called once per frame
    void Update()
    {
        h_move = Input.GetAxisRaw("Horizontal") * moveSpeed;
        if(Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            m_jump = true;
        }

    }

    private void FixedUpdate()
    {
        controller.Move(h_move*Time.fixedDeltaTime, m_jump);
        m_jump = false;

    }
}
