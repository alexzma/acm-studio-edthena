using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUnlock : MonoBehaviour
{
    static bool levelUnlockCreated = false;
    public bool level1 = true;
    public bool level2 = true;
    public bool level3 = true;
    public bool level4 = true;
    public bool level5 = true;
    // Start is called before the first frame update
    void Start()
    {
        if (!levelUnlockCreated)
        {
            DontDestroyOnLoad(this.gameObject);
            levelUnlockCreated = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Main Menu")
        {
            if (level1 == true)
            {
                GameObject.FindGameObjectWithTag("Level 1").GetComponent<Button>().interactable = true;
            }
            if (level2 == true)
            {
                GameObject.FindGameObjectWithTag("Level 2").GetComponent<Button>().interactable = true;
            }
            if (level3 == true)
            {
                GameObject.FindGameObjectWithTag("Level 3").GetComponent<Button>().interactable = true;
            }
            if (level4 == true)
            {
                GameObject.FindGameObjectWithTag("Level 4").GetComponent<Button>().interactable = true;
            }
            if (level5 == true)
            {
                GameObject.FindGameObjectWithTag("Level 5").GetComponent<Button>().interactable = true;
            }
        }
    }
}
