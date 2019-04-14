using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardController : MonoBehaviour
{
    public SpriteControllerv2 spriteController;
    public GameObject player;
    public GameObject explosion;
    private float timer = 0f;
    public float targetCD;
    private bool explosionHit = false;
    public float explosionRad = 2f;
    [SerializeField] private LayerMask m_whatIsPlayer;
    private bool explosionCompl = false;


    // Start is called before the first frame update
    void Start()
    {
        explosion.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(explosionCompl)
        {
            ExplosionTarget();
        }
        if (timer < Time.time)
        {
            ExplosionTrigger();
            if(explosionHit)
            {
                Debug.Log("oof Damaged");
                spriteController.Damage();
                explosionHit = false;
            }
        }
    }

    public void ExplosionTarget()
    {
        explosion.transform.position = player.transform.position;
        explosion.SetActive(true); //currently simulating turning on the explosion targetting animation
        timer = Time.time + targetCD;
        explosionCompl = false;
    }

    public void ExplosionTrigger()
    {
        //explosion animation happens here
        explosionHit = Physics2D.OverlapCircle(explosion.transform.position, explosionRad, m_whatIsPlayer);
        explosionCompl = true;
        explosion.SetActive(false);
    }
}
