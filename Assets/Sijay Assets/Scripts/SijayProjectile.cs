using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SijayProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "Platform")
        {
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "DestructibleProjectile")
        {
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "DestructibleProjectile2")
        {
            Destroy(other.gameObject);
        }
    }
}
