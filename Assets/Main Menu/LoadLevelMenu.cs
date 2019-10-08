using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelMenu : MonoBehaviour
{
    private float time = 10;
    private string level;
    public void loadScene(string level)
    {
        this.gameObject.GetComponent<AudioSource>().Play();
        this.level = level;
        time = 0;
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 0.5 && time <= 1)
        {
            SceneManager.LoadScene(level);
        }
    }
}
