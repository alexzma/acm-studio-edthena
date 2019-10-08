using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisCntl : MonoBehaviour
{
    public float spawnInterval;
    public float spawnCount;
    public GameObject debris;
    public Transform debrisSpawn;

    float nextSpawn = 0;
    int count = 0;

    void Update()
    {
        if (Time.time > nextSpawn && count < spawnCount)
        {
            Instantiate(debris, debrisSpawn);
            nextSpawn = Time.time + spawnInterval + Random.Range(0,3);
            count++;
        }
    }
}
