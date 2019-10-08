using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalAnim : MonoBehaviour
{


    Vector3 savePos;
    Rigidbody2D rb2D;
    public GameObject BubbleBackground;
    Vector2 speedVector;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = BubbleBackground.GetComponent<Rigidbody2D>();
        savePos = BubbleBackground.transform.position;
        speedVector = Vector2.up * speed;
        rb2D.velocity = speedVector;
        //rb2D.velocity = Vector2.SmoothDamp(rb2D.velocity, speedVector, ref m_velocity, smoothTime);
    }

    // Update is called once per frame
    void Update()
    {
        float offset = BubbleBackground.transform.position.y - savePos.y;
        if (offset > 9.9)
        {
            BubbleBackground.transform.position = savePos;
        }
    }
}
