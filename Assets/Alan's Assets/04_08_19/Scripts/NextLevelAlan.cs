using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextLevelAlan : MonoBehaviour
{
    public void LoadNextLevel(string level)
    {
        //Destroy(GameObject.FindGameObjectWithTag("Music"));
        FindObjectOfType<AudioManager>().Stop("Theme");
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(level);
    }
}
