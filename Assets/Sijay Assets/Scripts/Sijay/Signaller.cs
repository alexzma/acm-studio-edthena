using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signaller : MonoBehaviour
{
    public GameObject signallee;
    public string methodName;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hey");
        if (other.tag == "Player")
        {
            Debug.Log("hello0");
            signallee.SendMessage(methodName);
        }
    }
}
