using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    Animator bossAnimator;
    bool hit;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        bossAnimator = GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>();
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > 0.5 && hit)
        {
            bossAnimator.SetBool("Hit", false);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerBottom" && other.gameObject.GetComponentInParent<Rigidbody2D>().velocity.y < -5)
        {
            bossAnimator.SetBool("Hit", true);
            hit = true;
            time = 0;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
