using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpriteControllerBryant : MonoBehaviour
{

    //Animations
    Animator animator;
    const int STATE_IDLE = 0;
    const int STATE_RUN = 1;
    const int STATE_JUMP = 2;
    const int STATE_FALL = 3;
    const int STATE_DEAD = 4;
    const int STATE_SLIDE = 5;
    public int currentAnimState = STATE_IDLE;

    //Player Object Properties
    private Rigidbody2D m_rb2D;
    private SpriteRenderer m_spriteRenderer;

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
    const float m_wallRadius = 0.1f;
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

    float surviveTime;
    public bool facingRight = true;
    public bool invincible = false;
    public float levelTime = 360;
    public Transform onFloorTL;
    public Transform onFloorBR;
    public Text txt;

    private void Awake()
    {
        m_rb2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        surviveTime = Time.time + levelTime;
    }

    private void Update()
    {
        if (Time.time > surviveTime)
        {
            txt.text = "Nice.";
            Destroy(GameObject.Find("Eye1"));
            Destroy(GameObject.Find("Eye2"));
            Destroy(GameObject.Find("MinionSpawns"));
            Destroy(GameObject.Find("End"));
        }

        if (m_rb2D.transform.position.x < -55 || m_rb2D.transform.position.x > 55 ||
            m_rb2D.transform.position.y < -47 || m_rb2D.transform.position.y > 47)
        {
            isDead = true;
        }
    }

    //Called Each Physics Interaction
    public void FixedUpdate()
    {
        if (m_rb2D.velocity.x > 0) facingRight = true;
        else if (m_rb2D.velocity.x < 0) facingRight = false;

        m_grounded = Physics2D.OverlapArea(onFloorTL.position, onFloorBR.position, m_whatIsGround);
        m_onWall = Physics2D.OverlapCircle(m_wallCheck.position, m_wallRadius, m_whatIsWall);

        if (m_grounded)
        {
            m_airControl = true;
            m_doubleJump = true;
            m_slide = false;
        }
        
        if (m_onWall && !m_grounded)
        {
            m_wallJump = true;
            m_airControl = true;
            //Trying a new approach to Wall Jumping
            //Let the player reach the max height of jump
            //If RIGHT or LEFT arrow is being held when velocity.y changes from + to -
            //enter the sliding animation & set m_slide to true

            if ((currentAnimState == STATE_FALL && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))) || currentAnimState == STATE_SLIDE)
            {
                changeState(STATE_SLIDE);
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
        
        // Animation

        if (Mathf.Abs(m_rb2D.velocity.x) < 1 && m_grounded && !isDead)
        {
            changeState(STATE_IDLE);
        }
        else if (Mathf.Abs(m_rb2D.velocity.x) > 0 && m_grounded && !isDead)
        {
            changeState(STATE_RUN);
        }
        else if (m_rb2D.velocity.y > 0 && !m_grounded && !isDead)
        {
            changeState(STATE_JUMP);
        }
        else if (m_rb2D.velocity.y < 0 && !m_grounded && !isDead && !m_slide)
        {
            changeState(STATE_FALL);
        }
        else if (isDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

            //wallcheck.transform.position.x - player
            //if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            if (m_wallCheck.transform.position.x - m_rb2D.transform.position.x > 0)
            {
                m_rb2D.velocity = new Vector2(-walljumpX, walljumpY);
            }
            //else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            else if (m_wallCheck.transform.position.x - m_rb2D.transform.position.x < 0)
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
            case STATE_SLIDE:
                animator.SetInteger("state", STATE_SLIDE);
                break;
        }
        currentAnimState = state;
    }

    public bool takeDamage()
    {
        StartCoroutine("Recovery");
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
                break;
            default:
                return false;
        }
        health--;
        if (health == 0)
        {
            isDead = true;
        }
        return true;
    }

    IEnumerator Recovery()
    {
        invincible = true;
        for (int i = 0; i < 6; i++)
        {
            m_spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.25f);
            m_spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.25f);
        }
        invincible = false;
    }
}




/*
public GameObject platform;
if double jumped
    GameObject newPlatform = Instantiate(platform, platform.GetComponent<Transform>());
    newPlatform.transform.position = new Vector3(rb.position.x -1, rb.position.y, rb.position.z);
*/

/*  Current Value of the Player
 *  Transform.position (-3.52, -2.49., 0)
 *  Transform.Scale (40,40,1)
 * 
 *  Box Collider 2D size X=0.009, Y=0.016
 * 
 *  Rigidbody 2D
 *  Mass 1
 *  Gravity Scale 7
 *  
 *  Movement Controller
 *  MoveSpeed = 50
 * 
 *  Sprite Controllerv2
 *  Jump Force 750
 *  Smooth Speed 0.05
 *  Slide Speed 0.75
 *  Walljump CD 0.25
 *  Walljump X 10
 *  Walljump Y 15
 * 
 * 
 * 
 *  UPDATED VALUES of the Player 05/13/2019
 *  Transform.position (-3.52, -2.49, 0)
 *  Transform.Scale (40,40,1)
 * 
 *  Box Collider 2D size X=0.009, Y=0.016
 * 
 *  Rigidbody 2D
 *  Mass 1
 * ******************
 *  Gravity Scale 8 
 * ****************** 
 *  Movement Controller
 *  MoveSpeed = 50
 * 
 *  Sprite Controllerv2
 *  Jump Force 900
 *  Smooth Speed 0.05
 *  Slide Speed 0.75
 *  Walljump CD 0.25
 *  Walljump X 10
 *  Walljump Y 15
 * 
 */
