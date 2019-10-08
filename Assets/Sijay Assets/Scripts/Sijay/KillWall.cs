using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillWall : MonoBehaviour
{
    public float speed;
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (active)
        {
            transform.Translate(Vector3.right * speed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // this is where losing the game happens
            other.gameObject.SendMessage("Die");
        }
    }

    void Activate()
    {
        active = true;
    }
}
