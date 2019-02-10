using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int maxPlatforms = 3;
    public bool won = false;
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        int numPlatforms = platforms.Length;
        if(numPlatforms > maxPlatforms)
        {
            Destroy(platforms[0]);
        }
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        int numTargets = targets.Length;
        if(numTargets == 0)
        {
            won = true;
            Destroy(GameObject.FindGameObjectWithTag("Boss"));
        }
        if (!won && GameObject.FindGameObjectsWithTag("Projectile").Length != 1)
        {
            Instantiate(projectile);
        }
    }
}
