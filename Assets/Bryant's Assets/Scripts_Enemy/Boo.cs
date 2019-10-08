using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Uses bool in Player movement, tracking facing direction by key last pressed

public class Boo : MonoBehaviour
{
    public float speed = 2;

    public BoxCollider2D m_triggerT;
    public BoxCollider2D m_triggerB;

    GameObject player;
    SpriteControllerBryant p;
    Rigidbody2D playerRB;
    SpriteRenderer sr;

    private void Start()
    {
        player = GameObject.Find("Player");
        p = player.GetComponent<SpriteControllerBryant>();
        playerRB = player.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        bool facingRight = p.facingRight;
        Vector3 toPlayer = (player.transform.position - transform.position);//.normalized;
        if ((toPlayer.x < 0 && !facingRight) || (toPlayer.x > 0 && facingRight))
        {
            transform.position = transform.position + toPlayer * speed * Time.deltaTime;
        }

        if (toPlayer.x > 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (m_triggerT.IsTouching(col) && !m_triggerB.IsTouching(col) && playerRB.velocity.y < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
