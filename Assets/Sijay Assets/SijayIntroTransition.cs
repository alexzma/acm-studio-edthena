using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SijayIntroTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartLevel", 13.0f);
    }

    void StartLevel()
    {
        SceneManager.LoadScene("SijayLevel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
