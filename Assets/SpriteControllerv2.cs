﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteControllerv2 : MonoBehaviour
{
    //Controllers
    public GameController gameControl;
    public CameraController camControl;
    //public Arm_Rotation arm; //Not used in platformer
    public Camera m_sceneCamera;


    //Animations
    Animator animator;
    const int STATE_IDLE = 0;
    const int STATE_RUN = 1;
    const int STATE_JUMP = 2;
    const int STATE_FALL = 3;
    const int STATE_DEAD = 4;
    public int currentAnimState = STATE_IDLE;

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

    //Health System
    private int health = 3;
    private bool isDead = false;
    public GameObject Health3;
    public GameObject Health2;
    public GameObject Health1;

    private void Awake()
    {
        m_rb2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        animator = this.GetComponent<Animator>();
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
                m_slide = false;
            }



            if (m_onWall && !m_grounded)
            {
                m_wallJump = true;
                m_airControl = true;
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
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

            //Debug.Log(m_rb2D.velocity.x);

            if (Mathf.Abs(m_rb2D.velocity.x) < 1 && m_grounded && !isDead)
                changeState(STATE_IDLE);

            else if (Mathf.Abs(m_rb2D.velocity.x) > 0 && m_grounded && !isDead)
                changeState(STATE_RUN);

            else if (m_rb2D.velocity.y > 0 && !m_grounded && !isDead)
                changeState(STATE_JUMP);

            else if (m_rb2D.velocity.y < 0 && !m_grounded && !isDead)
                changeState(STATE_FALL);
            else if (isDead)
                changeState(STATE_DEAD);

            if(currentAnimState == STATE_DEAD)
        {
            m_rb2D.velocity = Vector2.zero;
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
        else if (!m_grounded && !m_slide && jump && m_doubleJump)
        {
            m_rb2D.velocity = new Vector2(m_rb2D.velocity.x, 0f);
            m_rb2D.AddForce(new Vector2(0f, m_jumpForce));
            m_doubleJump = false;
        }
        else if (!m_grounded && m_onWall && m_slide && jump && m_wallJump)
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
            m_doubleJump = false;
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


    //Camera Control
    //Comment out for other levels
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


    private void changeState(int state)
    {
        if (currentAnimState == state)
            return;

        switch (state)
        {
            case STATE_IDLE:
                animator.SetInteger("state", STATE_IDLE);
                break;
            case STATE_RUN:
                animator.SetInteger("state", STATE_RUN);
                break;
            case STATE_JUMP:
                animator.SetInteger("state", STATE_JUMP);
                break;
            case STATE_FALL:
                animator.SetInteger("state", STATE_FALL);
                break;
            case STATE_DEAD:
                animator.SetInteger("state", STATE_DEAD);
                break;
        }
        currentAnimState = state;
    }

    public bool Damage()
    {
        switch (health)
        {
            case 3:
                Health3.SetActive(false);
                break;
            case 2:
                Health2.SetActive(false);
                break;
            case 1:
                Health1.SetActive(false);
                isDead = true;
                break;
            default:
                return false;
        }
        health--;
        return true;
    }

}


/*
public GameObject platform;
if double jumped
    GameObject newPlatform = Instantiate(platform, platform.GetComponent<Transform>());
    newPlatform.transform.position = new Vector3(rb.position.x -1, rb.position.y, rb.position.z);
*/
