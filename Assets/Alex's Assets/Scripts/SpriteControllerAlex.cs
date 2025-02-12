﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteControllerAlex : MonoBehaviour
{
    //Controllers
    //public GameController gameControl;
    //public CameraController camControl;
    //public Arm_Rotation arm; //Not used in platformer
    //public Camera m_sceneCamera;

    //Animations
    public Animator animator;
    const int STATE_IDLE = 0;
    const int STATE_RUN = 1;
    const int STATE_JUMP = 2;
    const int STATE_FALL = 3;
    const int STATE_SLIDE = 4;
    public AudioSource walking;
    public int currentAnimState = STATE_IDLE;

    //Platform
    public GameObject platform;

    //Player Object Properties
    private Rigidbody2D m_rb2D;
    private SpriteRenderer m_spriteRenderer;

    //Camera Properties
    private float m_cameraOffset = 22.4f;
    //private float m_cameraOffset = 12.125f;

    //Movement Controls
    [SerializeField] private float m_jumpForce = 100f;
    [Range(0f, 0.3f)] [SerializeField] private float m_smoothSpeed = 0.05f;
    [Range(0f, 20f)] [SerializeField] private float m_slideSpeed = 5f;
    private Vector2 m_velocity = Vector2.zero;

    //Player Detection
    [SerializeField] private LayerMask m_whatIsGround;
    [SerializeField] private LayerMask m_whatIsWall;
    [SerializeField] private Transform m_groundCheck;
    [SerializeField] private Transform m_wallCheck;
    const float m_groundRadius = 0.2f;
    const float m_wallRadius = 0.2f;
    public bool m_airControl;
    public bool m_grounded;//Set public for Debug purposes; change to private later
    public bool m_onWall;//Set public for Debug purposes; change to private later
    //public bool m_inAir; //Set public for Debug purposes; change to private later
    private bool m_faceRight = true;
    public bool m_slide = false;

    //Jump Controls
    [SerializeField] private bool m_doubleJump = false;
    [SerializeField] private bool m_wallJump = false;
    private float timer = 0f;
    public float walljumpCD;
    public float walljumpX = 10f;
    public float walljumpY = 10f;


    private void Awake()
    {
        m_rb2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {

    }


    private void Update()
    {

    }

    //Called Each Physics Interaction
    public void FixedUpdate()
    {
        m_grounded = Physics2D.OverlapCircle(m_groundCheck.position, m_groundRadius, m_whatIsGround);
        m_onWall = Physics2D.OverlapCircle(m_wallCheck.position, m_wallRadius, m_whatIsWall);

        if (m_grounded)
        {
            //m_inAir = false;
            m_airControl = true;
            m_doubleJump = true;
        }
        //if (!m_grounded)
        //{
        //    //m_inAir = true;
        //}



        if (m_onWall)
        {
            m_wallJump = true;
            m_airControl = true;
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
            {
                m_rb2D.velocity = new Vector2(0f, -m_slideSpeed);
                m_slide = true;
            }
        }
        if (!m_onWall)
        {
            m_wallJump = false;
            m_slide = false;
        }

        if (!m_airControl)
        {
            if (Time.time >= timer)
            {
                m_airControl = true;
            }
        }


        if (Mathf.Abs(m_rb2D.velocity.x) < 1 && m_grounded)
        {
            animator.SetInteger("state", STATE_IDLE);
            walking.gameObject.SetActive(false);
        }
        else if (Mathf.Abs(m_rb2D.velocity.x) > 0 && m_grounded)
        {
            animator.SetInteger("state", STATE_RUN);
            walking.gameObject.SetActive(true);
        }
        else
        {
            walking.gameObject.SetActive(false);
            if (m_rb2D.velocity.y > 0 && !m_grounded)
                animator.SetInteger("state", STATE_JUMP);

            else if (m_rb2D.velocity.y < 0 && !m_grounded)
            {
                if (m_onWall && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
                {
                    animator.SetInteger("state", STATE_SLIDE);
                }
                else
                {
                    animator.SetInteger("state", STATE_FALL);
                }
            }
        }

    }

    public void Move(float movement, bool jump)
    {
        if (m_airControl)
        {
            //Horizontal movement using a constant speed
            Vector2 targetVelocity = new Vector2(movement * 10f, m_rb2D.velocity.y);
            m_rb2D.velocity = Vector2.SmoothDamp(m_rb2D.velocity, targetVelocity, ref m_velocity, m_smoothSpeed);
            if (movement > 0 && !m_faceRight && !m_onWall)
            {
                Flip();
            }
            else if (movement < 0 && m_faceRight && !m_onWall)
            {
                Flip();
            }
        }


        //Jumping Implementation
        if (m_grounded && jump)
        {
            m_rb2D.AddForce(new Vector2(0f, m_jumpForce));
            m_grounded = false;
        }
        else if (!m_grounded && !m_onWall && jump && m_doubleJump)
        {
            m_rb2D.velocity = new Vector2(m_rb2D.velocity.x, 0f);
            m_rb2D.AddForce(new Vector2(0f, m_jumpForce));
            GameObject newPlatform = Instantiate(platform, platform.GetComponent<Transform>());
            newPlatform.transform.position = new Vector3(m_rb2D.position.x, m_rb2D.position.y - 1, 0);
            m_doubleJump = false;
        }
        else if (!m_grounded && m_onWall && jump && m_wallJump)
        {
            m_rb2D.velocity = new Vector2(m_rb2D.velocity.x, 0f);

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                m_rb2D.velocity = new Vector2(-walljumpX, walljumpY);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                m_rb2D.velocity = new Vector2(walljumpX, walljumpY);
            }

            Flip();
            m_onWall = false;
            m_wallJump = false;
            //m_doubleJump = false;
            m_airControl = false;
            timer = Time.time + walljumpCD;
        }
    }

    //Flip the player sprite & axis
    private void Flip()
    {
        //arm.flip(); //Not used in platformer
        m_faceRight = !m_faceRight;

        Vector2 scale = transform.localScale;
        scale.x = -1 * scale.x;
        transform.localScale = scale;
    }

    /*
    //Camera Control
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CameraTrigger")
        {
            //If the player is at the farthest right camera edge
            if (camControl.cameraOffset < 0f)
            {
                m_sceneCamera.transform.position = new Vector3(m_sceneCamera.transform.position.x + m_cameraOffset, m_sceneCamera.transform.position.y, m_sceneCamera.transform.position.z);
            }
            //If the player is at the farthest left camera edge
            else if (camControl.cameraOffset > 0f)
            {
                m_sceneCamera.transform.position = new Vector3(m_sceneCamera.transform.position.x - m_cameraOffset, m_sceneCamera.transform.position.y, m_sceneCamera.transform.position.z);
            }
        }
    }
    */


}


