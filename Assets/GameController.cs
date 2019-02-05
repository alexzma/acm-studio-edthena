using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int maxPlatforms = 3;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("New Platform");
        int numPlatforms = objects.Length;
        if(numPlatforms > maxPlatforms)
        {
            Destroy(objects[0]);
        }
    }
}
