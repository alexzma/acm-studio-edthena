using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int maxPlatforms = 3;
    public LevelController level;
    private bool won = false;
    private bool stage3 = false;
    private bool stage2 = false;
    public GameObject projectile;
    public int maxProjectiles = 21;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        level.OnLevelLoaded += OnLoad;
        level.OnStageOneCompletion += OnStageTwo;
        level.OnStageTwoCompletion += OnStageThree;
        level.OnStageThreeCompletion += OnWin;
    }

    void OnLoad()
    {
        print("Hello");
    }

    void OnStageTwo()
    {
        stage2 = true;
        Transform transform = GameObject.FindGameObjectWithTag("Boss").transform;
        Vector3 projPos = new Vector3(transform.position.x, transform.position.y, 0);
        target.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, new Quaternion(0, 0, 0, 0));
        Instantiate(target);
        projPos = new Vector3(transform.position.x - 10, transform.position.y, 0);
        target.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, new Quaternion(0, 0, 0, 0));
        Instantiate(target);
        projPos = new Vector3(transform.position.x + 10, transform.position.y, 0);
        target.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, new Quaternion(0, 0, 0, 0));
        Instantiate(target);
        projPos = new Vector3(transform.position.x, transform.position.y - 4, 0);
        target.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, new Quaternion(0, 0, 0, 0));
        Instantiate(target);
        projPos = new Vector3(transform.position.x + 10, transform.position.y - 4, 0);
        target.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, new Quaternion(0, 0, 0, 0));
        Instantiate(target);
        projPos = new Vector3(transform.position.x - 10, transform.position.y - 4, 0);
        target.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, new Quaternion(0, 0, 0, 0));
        Instantiate(target);
    }

    void OnStageThree()
    {
        stage3 = true;
        Transform transform = GameObject.FindGameObjectWithTag("Boss").transform;
        Vector3 projPos = new Vector3(transform.position.x - 10, transform.position.y, 0);
        target.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, new Quaternion(0, 0, 0, 0));
        Instantiate(target);
        projPos = new Vector3(transform.position.x + 10, transform.position.y, 0);
        target.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, new Quaternion(0, 0, 0, 0));
        Instantiate(target);
        projPos = new Vector3(transform.position.x, transform.position.y - 5, 0);
        target.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, new Quaternion(0, 0, 0, 0));
        Instantiate(target);
        projPos = new Vector3(transform.position.x, transform.position.y + 5, 0);
        target.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, new Quaternion(0, 0, 0, 0));
        Instantiate(target);
    }

    void OnWin()
    {
        won = true;
        Destroy(GameObject.FindGameObjectWithTag("Boss"));
        Destroy(GameObject.FindGameObjectWithTag("Projectile"));
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("DestructibleProjectile");
        for (int i = 0; i < projectiles.Length; i++)
        {
            Destroy(projectiles[i]);
        }
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
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("DestructibleProjectile");
        int numProjectiles = projectiles.Length;
        if (numProjectiles < maxProjectiles && stage2 && !won)
        {
            float position = Random.value*40;
            Transform transform = GameObject.FindGameObjectWithTag("Boss").transform;
            Vector3 projPos = new Vector3(position - 20, transform.position.y+10, 0);
            projectile.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, new Quaternion(0, 0, 0, 0));
            Instantiate(projectile);
        }
    }
}
