using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlexIntroVideoScript : MonoBehaviour
{
    private float time;
    public string level;
    public GameObject sound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 15)
        {
            Instantiate(sound);
            SceneManager.LoadScene(level);
        }
    }
}
