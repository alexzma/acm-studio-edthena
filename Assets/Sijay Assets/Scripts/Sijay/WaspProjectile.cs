using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspProjectile : MonoBehaviour
{
    public float projectileSpeed = 1.0f;
    public float projectileDistance = 5.0f;
    private Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.right * projectileSpeed);
        if ((transform.position - initialPos).magnitude > projectileDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall" || other.tag == "Platform" || other.tag == "Projectile")
        {
            Destroy(gameObject);
        }
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            other.SendMessage("OnHit");
        }
    }
}
