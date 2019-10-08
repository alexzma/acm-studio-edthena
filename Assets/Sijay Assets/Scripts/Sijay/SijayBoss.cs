using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SijayBoss : MonoBehaviour
{
    public GameObject levelController;
    public float attackInterval;
    public GameObject player;
    public GameObject bomb;
    public GameObject vine;
    public float bombLatitude;
    public float bombHeight;
    public float bombWidth;
    public float vineDistance;
    private int health = 4;
    private Animator anim;
    public AudioSource screech;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Activate()
    {
        InvokeRepeating("Attack", attackInterval, attackInterval);
    }

    void Damage()
    {
        anim.SetTrigger("damage");
        health--;
        Debug.Log(health);
        if (health == 0)
        {
            Die();
        }
    }

    void Die()
    {
        levelController.SendMessage("PlayerWon");
    }

    void Attack()
    {
        switch(Random.Range(0, 2))
        {
            case 0:
                // instantiate a couple wasp bombs
                anim.SetTrigger("attack1");
                Invoke("DropBombs", 2);
                break;
            case 1:
                // instantiate a couple vine whips
                anim.SetTrigger("attack2");
                screech.Play();
                ShootVines();
                break;
        }
    }

    void DropBombs()
    {
        float x1 = Random.Range(-bombLatitude, -bombWidth);
        float x2 = Random.Range(bombWidth, bombLatitude);

        Vector3 h1 = new Vector2(x1, bombHeight);
        Vector3 h2 = new Vector2(x2, bombHeight);

        Vector3 v1 = player.transform.position + h1;
        Vector3 v2 = player.transform.position + h2;

        if (Physics2D.Raycast(player.transform.position, h1, h1.magnitude, LayerMask.GetMask("Platform")).collider == null) 
            Instantiate(bomb, v1, Quaternion.identity);
        if (Physics2D.Raycast(player.transform.position, h2, h2.magnitude, LayerMask.GetMask("Platform")).collider == null)
            Instantiate(bomb, v2, Quaternion.identity);
        Debug.Log(Physics2D.Raycast(player.transform.position, h1, h1.magnitude).collider);
    }

    void ShootVines()
    {
        float angle = Random.Range(0.0f, 360.0f);
        Quaternion rot1 = Quaternion.AngleAxis(angle, Vector3.forward);
        Quaternion rot2 = Quaternion.AngleAxis(angle + Random.Range(30.0f, 60.0f), Vector3.forward);

        Instantiate(vine, player.transform.position - rot1 * Vector3.right * vineDistance * 2, rot1);
        Instantiate(vine, player.transform.position - rot2 * Vector3.right * vineDistance * 2, rot2);
    }
}
