using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardController : MonoBehaviour
{
    //Imported Objects
    public SpriteControllerv2 spriteController;
    public GameObject player;
    public GameObject explosionAnim;
    public GameObject target;


    //Timers
    private float timer = 0f;
    private float explosionTimer = 0f;

    //CDs
    public float explosionDuration;
    public float targetCD;


    //Explosion Properties
    private bool explosionHit = false;
    public float explosionRad = 2f;
    [SerializeField] private LayerMask m_whatIsPlayer;
    private bool explosionCompl;

    //Not exactly needed
    Animator animator;


    //Debug
    bool triggerOnce = true;
    bool activated;
    // Start is called before the first frame update
    void Start()
    {
        //explosion.SetActive(false);
        target.SetActive(false);
        explosionAnim.SetActive(false);
        animator = explosionAnim.GetComponent<Animator>();
        ExplosionTarget();
        //explosionCompl = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(explosionCompl)
        {
            ExplosionTarget();
        }
        else if (timer < Time.time)
        {
            //if (triggerOnce)
            //{
            if (activated)
            {
                ExplosionTrigger();
                activated = false;
            }

            if (explosionHit)
            {
                //Debug.Log("oof Damaged");
                spriteController.Damage();
                explosionHit = false;
            }

            if (explosionTimer < Time.time)
            {
                //Debug.Log("Duration Condition Enter");
                explosionCompl = true;
                explosionAnim.SetActive(false);
            }

            //triggerOnce = false;
            //}
            //Debug.Log("explosion timer");
            //Debug.Log(explosionTimer);
            //Debug.Log("Real Time");
            //Debug.Log(Time.time);



        }
    }

    public void ExplosionTarget()
    {
        target.transform.position = player.transform.position;
        explosionAnim.transform.position = target.transform.position;
        target.SetActive(true); //currently simulating turning on the explosion targetting animation
        timer = Time.time + targetCD;
        explosionCompl = false;
        //explosionAnim.SetActive(false);

        //Debug
        activated = true;
    }

    public void ExplosionTrigger()
    {
        //explosion animation happens here
        //explosion.SetActive(true); //used for now bc Anim is a chile of explosion
        //animator.SetBool("explode", true);
        target.SetActive(false);
        explosionAnim.SetActive(true);
        explosionHit = Physics2D.OverlapCircle(explosionAnim.transform.position, explosionRad, m_whatIsPlayer);
        explosionTimer = Time.time + explosionDuration;
        FindObjectOfType<AudioManager>().Play("Explosion");
        //explosionCompl = true;
        //animator.SetBool("explode", false);
        //explosion.SetActive(false);
    }
}
