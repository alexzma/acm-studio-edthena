using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndVideoScript : MonoBehaviour
{
    private float time;
    public GameObject credits;
    public GameObject menuButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= 32)
        {
            credits.SetActive(true);
            menuButton.SetActive(true);

        }
    }
}
