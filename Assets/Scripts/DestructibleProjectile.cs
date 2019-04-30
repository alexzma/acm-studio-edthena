using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleProjectile : MonoBehaviour
{
    public GameObject particles;
    public AudioSource flame;
    bool isPlaying = false;
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!(flame.isPlaying) && isPlaying)
        {
            Destroy(this.gameObject);
        }
        time += Time.deltaTime;
        if(time >= 15)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        if(other.gameObject.tag == "Projectile")
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Platform" && !isPlaying)
        {
            isPlaying = true;
            Vector3 projPos = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
            particles.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, Quaternion.Euler(-90, 0, 0));
            Instantiate(particles);
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            EdgeCollider2D[] s = this.gameObject.GetComponents<EdgeCollider2D>();
            for(int i = 0; i < s.Length; i++)
            {
                s[i].enabled = false;
            }
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            flame.Play();
        }
    }
}
