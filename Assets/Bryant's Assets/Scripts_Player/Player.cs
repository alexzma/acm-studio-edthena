using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpStr = 10.0f;
    public float projectileLaunchSpeed = 10.0f;

    public Transform onFloorTL; // Top Left of ground check rect
    public Transform onFloorBR; // Box Right of ground check rect
    public LayerMask GroundLayerMask;

    bool doJump;
    bool canDoubleJump;
    bool isGrounded;
    public bool facingRight;

    Rigidbody2D rb;

    // enum -> player's selected platform
    // All this enum shit sloppy, make own class or something
    public GameObject[] playerPlatObjects;
    enum PlayerPlats
    {
        cubeS,
        cubeM,
        cubeB,
        rectHor,
        rectVer
    };
    PlayerPlats selectedPlat;
    private GameObject getPlayerPlatObject()
    {
        return playerPlatObjects[(int)selectedPlat];
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        selectedPlat = PlayerPlats.cubeS;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapArea(onFloorTL.position, onFloorBR.position, GroundLayerMask);

        // Jumping
        if (isGrounded) canDoubleJump = true;
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)                     // First jump
            {
                doJump = true;
            }
            else if (!isGrounded && canDoubleJump)    // Second Jump
            {
                doJump = true;
                canDoubleJump = false;
            }
        }

        // Attack
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isGrounded)
        {
            GameObject obj = Instantiate(getPlayerPlatObject(), new Vector3(transform.position.x - 1, transform.position.y, 0), transform.rotation);
            // change out getcomponent later, maybe use object types?
            obj.GetComponent<Rigidbody2D>().AddForce(Vector2.left * projectileLaunchSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !isGrounded)
        {
            GameObject obj = Instantiate(getPlayerPlatObject(), new Vector3(transform.position.x + 1, transform.position.y, 0), transform.rotation);
            obj.GetComponent<Rigidbody2D>().AddForce(Vector2.right * projectileLaunchSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isGrounded)
        {
            GameObject obj = Instantiate(getPlayerPlatObject(), new Vector3(transform.position.x, transform.position.y - 1, 0), transform.rotation);
            obj.GetComponent<Rigidbody2D>().AddForce(Vector2.down * projectileLaunchSpeed);
        }

        // Platform Selection
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            selectedPlat = (PlayerPlats)(((int)selectedPlat + 1) % playerPlatObjects.Length);
        }
    }

    private void FixedUpdate()
    {
        // Horizontal
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);
        if (rb.velocity.x > 0) facingRight = true;
        else if (rb.velocity.x < 0) facingRight = false;

        // Jump
        if (doJump)
        {
            doJump = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpStr);
        }
    }

    public void takeDamage()
    {
        Debug.Log("Player hit. OWie");
    }
}

