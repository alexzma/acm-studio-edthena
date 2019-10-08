using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : MonoBehaviour
{
    public float smoothTime = 3.0f;
    public float moveInterval = 3.0f;
    public float moveRange = 2.0f;
    private GameObject player;
    public GameObject projectile;
    public float attackRange = 4.0f;
    public float shootInterval = 2.0f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition;
    private Vector3 initialPosition;



    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Start()
    {
        initialPosition = transform.position;
        targetPosition = new Vector3(initialPosition.x + Random.Range(-moveRange, moveRange), initialPosition.y + Random.Range(-moveRange, moveRange), 0);
        InvokeRepeating("NewTargetPosition", moveInterval, moveInterval);
        InvokeRepeating("FireProjectile", shootInterval, shootInterval);
    }

    // Update is called once per frame
    void Update()
    {
        int sec = (int) Time.time;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        if ((player.transform.position.x - transform.position.x) < 0f)
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
    }

    void NewTargetPosition()
    {
        targetPosition = new Vector3(initialPosition.x + Random.Range(-moveRange, moveRange), initialPosition.y + Random.Range(-moveRange, moveRange), 0);
    }

    void OnPress()
    {
        Destroy(gameObject);
    }

    void FireProjectile()
    {
        if ((player.transform.position - transform.position).magnitude < attackRange)
        {
            Vector3 toPlayer = player.transform.position - transform.position;
            Quaternion rot = Quaternion.AngleAxis(Vector3.SignedAngle(Vector3.right, toPlayer, Vector3.forward), Vector3.forward);
            Instantiate(projectile, transform.position + rot * Vector3.right, rot);//Quaternion.LookRotation(toPlayer, Vector3.up));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        targetPosition = transform.position;
    }
}
