using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelEnd : MonoBehaviour
{
    public void loadScene(string level)
    {
        SceneManager.LoadScene(level);
    }
}
