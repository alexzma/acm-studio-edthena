using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BryantIntroTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartLevel", 14.0f);
    }

    void StartLevel()
    {
        SceneManager.LoadScene("Level");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
