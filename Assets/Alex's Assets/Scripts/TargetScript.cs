using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    Animator bossAnimator;
    Light light;
    bool hit;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        bossAnimator = GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>();
        light = GameObject.FindGameObjectWithTag("Light").GetComponent<Light>();
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > 0.05 && hit)
        {
            light.intensity = 10;
        }
        if(time > 0.1 && hit)
        {
            light.intensity = 100;
        }
        if (time > 0.15 && hit)
        {
            light.intensity = 1000;
        }
        if (time > 0.2 && hit)
        {
            light.intensity = 100;
        }
        if (time > 0.25 && hit)
        {
            light.intensity = 10;
        }
        if (time > 0.3 && hit)
        {
            light.intensity = 1;
        }
        if (time > 0.5 && hit)
        {
            bossAnimator.SetBool("Hit", false);
        }
        if (time > 1 && hit)
        {
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
            gameObject.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
