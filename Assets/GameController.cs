using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

   
    public GameObject player;

    private bool paused = false;

    private void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.P))
        //{
        //    Time.timeScale = 0;
        //    paused = true;
        //}
        //if(Input.GetKeyDown(KeyCode.P) && paused)
        //{
        //    Time.timeScale = 1;
        //}
    }
}
