using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public float sideForce = 1000;
    public float jumpForce = 500;
    private int jumped = 0;
    private Rigidbody2D rb;
    public GameObject platform;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(new Vector2(-1, 0)*sideForce*Time.deltaTime, ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(new Vector2(1, 0)*sideForce*Time.deltaTime, ForceMode2D.Force);
        }
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && jumped < 2)
        {
            rb.AddForce(new Vector2(0, 1) * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
            jumped++;
            if(jumped == 2)
            {
                //Create platform
                GameObject newPlatform = Instantiate(platform, platform.GetComponent<Transform>());
                newPlatform.transform.position = new Vector3(rb.position.x -1, rb.position.y - 1, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumped = 0;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        jumped = 1;
    }
}
