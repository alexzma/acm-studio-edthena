using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBlock : MonoBehaviour
{
    public float timeToFall = 1.0f;
    public float fallSpeed = 1.0f;
    public float respawnTime = 1.0f;
    public BoxCollider2D m_trigger;

    Rigidbody2D rb;
    SpriteRenderer sr;
    bool steppedOn = false;
    float timeSteppedOn;
    Vector3 startPos;

    //float LerpL, LerpR;
    //static float t = 0.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        startPos = transform.position;
        //LerpL = transform.position.x - .2f;
        //LerpR = transform.position.x + .2f;
    }

    private void Update()
    {
        if (steppedOn)
        {
            if (Time.time >= timeSteppedOn + timeToFall)
            {
                rb.gravityScale = 1.0f;
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                //rb.MovePosition(new Vector2(transform.position.x, transform.position.y - fallSpeed));
            }

            //t += 1f * Time.deltaTime;
            //transform.position = new Vector2(Mathf.Lerp(LerpL, LerpR, t), transform.position.y);
            //if (t > 1.0f)
            //{
            //    float temp = LerpR;
            //    LerpR = LerpL;
            //    LerpL = temp;
            //    t = 0.0f;
            //}
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            timeSteppedOn = Time.time;
            steppedOn = true;
            m_trigger.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Player")       // set to ground layer index
        {
            steppedOn = false;
            StartCoroutine("Flicker");
        }
    }

    IEnumerator Flicker()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.5f);
            sr.enabled = false;
            yield return new WaitForSeconds(0.5f);
            sr.enabled = true;
        }
        this.enabled = false;
        yield return new WaitForSeconds(respawnTime);
        transform.position = startPos;
        steppedOn = false;
        m_trigger.enabled = true;
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        this.enabled = true;
    }
}
