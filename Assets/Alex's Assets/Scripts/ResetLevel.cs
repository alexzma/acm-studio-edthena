using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    private float time = 10;
    private string level;
    public void reloadLevel()
    {
        level = SceneManager.GetActiveScene().name;
        this.gameObject.GetComponent<AudioSource>().Play();
        time = 0;
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 0.5 &&  time <= 1)
        {
            SceneManager.LoadScene(level);
        }
    }
}
