using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour
{
    public float vineSpeed;
    public float vineDistance;
    public float pauseTime;
    private float dist = 0f;
    

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
        if (dist < vineDistance)
        {
            dist += vineSpeed;
            transform.Translate(Vector3.right * vineSpeed);
            Invoke("Die", pauseTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.SendMessage("OnHit");
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
