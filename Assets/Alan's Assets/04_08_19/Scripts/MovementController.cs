using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    //public SpriteControllerVelocity controller;
    public SpriteControllerv2 controller;
    public GameObject player;

    //public RespawnController spawnControl;
    private float h_move = 0f;
    private bool m_jump = false;

    private bool paused;

    [Range (0,100)][SerializeField] private float moveSpeed = 50f;

    // Update is called once per frame
    void Update()
    {
        if (controller.currentAnimState != 4)
        {
            h_move = Input.GetAxisRaw("Horizontal") * moveSpeed;
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            {
                m_jump = true;
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                controller.Damage();
                Debug.Log("oof Damaged");
            }
            //if (Mathf.Abs(controller.m_rb2D.velocity.x) > 0 && controller.)
            //controller.changeState(controller.S);
        }
    }

    private void FixedUpdate()
    {
        if (controller.currentAnimState != 4)
        {
            controller.Move(h_move * Time.fixedDeltaTime, m_jump);
            m_jump = false;
        }
    }
}
