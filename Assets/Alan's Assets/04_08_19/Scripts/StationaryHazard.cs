using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryHazard : MonoBehaviour
{
    //Imported Objects
    public SpriteControllerv2 spriteController;
    //public GameObject player;
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

    //Player info for Sounds
    public bool BossRoom;
    public int stageNumber;
    public AudioSource source;

    //Debug
    bool triggerOnce = true;
    bool activated;
    // Start is called before the first frame update
    void Start()
    {
        stageNumber = spriteController.stageNum;
        target.SetActive(false);
        explosionAnim.SetActive(false);
        ExplosionTarget();
        source.enabled= false;
    }

    // Update is called once per frame
    void Update()
    {
        stageNumber = spriteController.stageNum;

        if (explosionCompl)
        {
            ExplosionTarget();
        }
        else if (timer < Time.time)
        {
            if (activated)
            {
                ExplosionTrigger();
                activated = false;
            }

            if (explosionHit)
            {
                spriteController.Damage();
                explosionHit = false;
            }

            if (explosionTimer < Time.time)
            {
                explosionCompl = true;
                explosionAnim.SetActive(false);
            }

        }
    }

    public void ExplosionTarget()
    {
        target.SetActive(true); //currently simulating turning on the explosion targetting animation
        timer = Time.time + targetCD;
        explosionCompl = false;
        activated = true;
        source.enabled = false;
    }

    public void ExplosionTrigger()
    {
        target.SetActive(false);
        explosionAnim.SetActive(true);
        explosionHit = Physics2D.OverlapCircle(explosionAnim.transform.position, explosionRad, m_whatIsPlayer);
        explosionTimer = Time.time + explosionDuration;
        if(stageNumber == 1 && !BossRoom)
        {
            source.enabled = true;
            //FindObjectOfType<AudioManager>().Play("Explosion");
        }
        else if(stageNumber == 2 && BossRoom)
        {
            source.enabled = true;
            //FindObjectOfType<AudioManager>().Play("Explosion");
        }
    }
}
