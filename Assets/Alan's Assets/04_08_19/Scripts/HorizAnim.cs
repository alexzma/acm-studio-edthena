using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizAnim : MonoBehaviour
{
  
   
    Vector3 savePos;
    Rigidbody2D rb2D;
    public GameObject FishBackground;
    Vector2 speedVector;
    public float speedFISH;
   


    // Start is called before the first frame update
    void Start()
    {
        
        rb2D = FishBackground.GetComponent<Rigidbody2D>();
        rb2D.velocity = Vector2.zero;
        savePos = FishBackground.transform.position;
        speedVector = Vector2.left * speedFISH;
        rb2D.velocity = speedVector;
        //rb2D.velocity = Vector2.SmoothDamp(rb2D.velocity, speedVector, ref m_velocity, smoothTime);
    }

    // Update is called once per frame
    void Update()
    {
        float offset = savePos.x - FishBackground.transform.position.x;
        if (offset > 17.8)
        {
            FishBackground.transform.position = savePos;
        }
    }
}
