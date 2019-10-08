using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspSpawner : MonoBehaviour
{
    public bool spawnUp;
    public bool spawnDown;
    public bool spawnLeft;
    public bool spawnRight;

    public float spawnDistance;

    public int health = 3;

    public GameObject wasp;
    public GameObject boss;

    private GameObject upSpawn;
    private GameObject downSpawn;
    private GameObject leftSpawn;
    private GameObject rightSpawn;
    public float spawnInterval;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnWasp", spawnInterval, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnWasp()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                if (spawnUp && upSpawn == null)
                {
                    upSpawn = Instantiate(wasp, transform.position + Vector3.up * spawnDistance, transform.rotation);
                }
                break;
            case 1:
                if (spawnDown && downSpawn == null)
                {
                    downSpawn = Instantiate(wasp, transform.position + Vector3.down * spawnDistance, transform.rotation);
                }
                break;
            case 2:
                if (spawnRight && rightSpawn == null)
                {
                    rightSpawn = Instantiate(wasp, transform.position + Vector3.right * spawnDistance, transform.rotation);
                }
                break;
            case 3:
                if (spawnLeft && leftSpawn == null)
                {
                    leftSpawn = Instantiate(wasp, transform.position + Vector3.left * spawnDistance, transform.rotation);
                }
                break;
        }
    }

    void OnPress()
    {
        health--;
        if (health == 0)
        {
            boss.SendMessage("Damage");
            Destroy(gameObject);
        }
    }
}
