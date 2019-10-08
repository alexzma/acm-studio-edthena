using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspBomb : MonoBehaviour
{
    private float fallDistance;
    public float fallSpeed;
    public float minDistance;
    public float maxDistance;
    public GameObject wasp;
    private float initialY;
    private Animator anim;
    private bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        initialY = transform.position.y;
        fallDistance = Random.Range(minDistance, maxDistance);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (!done)
        {
            transform.Translate(Vector3.down * fallSpeed);
            if (Mathf.Abs(transform.position.y - initialY) > fallDistance)
            {
                SpawnWasp();
                done = true;
                Invoke("Des", 0.5f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!done)
        {
            SpawnWasp();
            done = true;
            Invoke("Des", 0.5f);
        }
    }

    void Des()
    {
        Destroy(gameObject);
    }

    void SpawnWasp()
    {
        anim.SetTrigger("explode");
        Instantiate(wasp, transform.position, Quaternion.identity);
    }
}
