using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SijaySpriteControllerv2 : MonoBehaviour
{
    //Controllers
    //public GameController gameControl;
    //public CameraController camControl;
    //public Arm_Rotation arm; //Not used in platformer
    //public Camera m_sceneCamera;

    public GameObject levelController;
    private int health = 3;
    public float hit_invuln_time = 2.0f;
    private float invuln_time = 0.0f;

    public Animator animator;
    public AudioSource steppingSound;
    public AudioSource deathSound;

    private bool dead = false;

    //Platform
    public GameObject platform;
    public GameObject projectile;

    //Player Object Properties
    private Rigidbody2D m_rb2D;
    private SpriteRenderer m_spriteRenderer;

    //Camera Properties
    private float m_cameraOffset = 22.4f;
    //private float m_cameraOffset = 12.125f;

    //Movement Controlsd
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
    [SerializeField] private float m_fastfallForce = 200f;
    private float timer = 0f;
    public float walljumpCD;
    public float walljumpX = 10f;
    public float walljumpY = 10f;
    private bool m_fastfall = false;
    public bool m_fired = false;

    private void Awake()
    {
        m_rb2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        deathSound.gameObject.SetActive(false);
    }

    private void Start()
    {

    }


    private void Update()
    {
        if (dead) { return; }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_fastfall = true;
            animator.SetBool("Diving", true);
            if (!m_grounded && !m_fired)
            {
                FireProjectile();
                m_fired = true;
            }
        }
    }

    //Called Each Physics Interaction
    public void FixedUpdate()
    {
        if (invuln_time > 0)
        {
            invuln_time -= Time.deltaTime;
        }
        if (invuln_time < 0)
        {
            invuln_time = 0.0f;
        }

        animator.SetFloat("VerticalVelocity", m_rb2D.velocity.y);

        Collider2D ground = Physics2D.OverlapCircle(m_groundCheck.position, m_groundRadius, m_whatIsGround);
        m_grounded = ground;
        if (m_grounded && ground.gameObject.tag == "Platform")
        {
            //transform.parent = Physics2D.OverlapCircle(m_groundCheck.position, m_groundRadius, m_whatIsGround).transform;
        }
        else
        {
            transform.parent = null;
        }
        m_onWall = Physics2D.OverlapCircle(m_wallCheck.position, m_wallRadius, m_whatIsWall);

        if (m_grounded)
        {
            //m_inAir = false;
            m_airControl = true;
            m_doubleJump = true;
            m_fastfall = false;
            animator.SetBool("Diving", false);
            m_fired = false;
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





    }

    public void Move(float movement, bool jump)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("RUN"))
        {
            steppingSound.gameObject.SetActive(true);
        } else
        {   
            steppingSound.gameObject.SetActive(false);
        }   

        if (dead) { movement = 0; jump = false; }
        if (m_airControl)
        {
            //Horizontal movement using a constant speed
            Vector2 targetVelocity = new Vector2(movement * 10f, m_rb2D.velocity.y);
            m_rb2D.velocity = Vector2.SmoothDamp(m_rb2D.velocity, targetVelocity, ref m_velocity, m_smoothSpeed);
            animator.SetFloat("Speed", Mathf.Abs(targetVelocity.x));
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
            //GameObject newPlatform = Instantiate(platform, platform.GetComponent<Transform>());
            //newPlatform.transform.position = new Vector3(m_rb2D.position.x, m_rb2D.position.y - 1, 0);
            m_doubleJump = false;
            m_fastfall = false;
            animator.SetBool("Diving", false);
            m_fired = false;
        }
        else if (!m_grounded && m_onWall && jump && m_wallJump)
        {
            m_rb2D.velocity = new Vector2(m_rb2D.velocity.x, 0f);

            if (Input.GetKey(KeyCode.RightArrow))
            {
                m_rb2D.velocity = new Vector2(-walljumpX, walljumpY);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                m_rb2D.velocity = new Vector2(walljumpX, walljumpY);
            }

            Flip();
            m_onWall = false;
            m_wallJump = false;
            //m_doubleJump = false;
            m_airControl = false;
            timer = Time.time + walljumpCD;
            m_fastfall = false;
            animator.SetBool("Diving", false);
            m_fired = false;
        }
        if (!m_grounded && !animator.GetBool("Jumping"))
        {
            animator.SetBool("Jumping", true);
        }
        else if (m_grounded && animator.GetBool("Jumping"))
        {
            animator.SetBool("Jumping", false);
        }

        // fastfall implementation
        if (m_fastfall)
        {
            m_rb2D.velocity = new Vector2(m_rb2D.velocity.x, -m_fastfallForce);
            //m_rb2D.AddForce(Vector2.down * m_fastfallForce);
            m_fastfall = false;
            animator.SetBool("Diving", false);
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

    private void FireProjectile()
    {
        float offset = 0.7f;
        float rotation = 0;
        if (!m_faceRight)
        {
            offset *= -1;
            rotation = 180;
        }
        Vector3 pos = new Vector3(m_rb2D.position.x + offset, m_rb2D.position.y, 0);
        GameObject newProjectile = Instantiate(projectile, pos, Quaternion.AngleAxis(rotation, Vector3.up));
    }

    void OnHit()
    {
        if (invuln_time <= 0.0)
        {
            health--;
            if (health == 0)
            {
                Die();
            }
            invuln_time = hit_invuln_time;
        }
    }

    public void Invul()
    {
        health = -1;
    }

    void Die()
    {
        dead = true;
        animator.SetTrigger("die");
        deathSound.gameObject.SetActive(true);
        Invoke("ResetLevel", 1.2f);
    }

    void ResetLevel()
    {
        levelController.SendMessage("PlayerDied");
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

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.ToString());
        if (other.gameObject.tag == "Platform")
        {
            Debug.Log("HEY");
            transform.parent = other.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }
    */
}


