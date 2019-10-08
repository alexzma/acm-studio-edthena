using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialNext : MonoBehaviour
{
    public GameObject ThisIsYou;
    public GameObject OriginalPlatform;
    public GameObject BreakThis;
    public GameObject CreateWithDoubleJump;
    public GameObject DodgeThis;
    public GameObject DontTouch1;
    public GameObject DontTouch2;
    private int numClicked;
    // Start is called before the first frame update
    void Start()
    {
        numClicked = 0;
    }

    public void Next()
    {
        switch (numClicked)
        {
            case 0:
                ThisIsYou.SetActive(false);
                OriginalPlatform.SetActive(true);
                break;
            case 1:
                OriginalPlatform.SetActive(false);
                BreakThis.SetActive(true);
                break;
            case 2:
                BreakThis.SetActive(false);
                CreateWithDoubleJump.SetActive(true);
                break;
            case 3:
                CreateWithDoubleJump.SetActive(false);
                DodgeThis.SetActive(true);
                break;
            case 4:
                DodgeThis.SetActive(false);
                DontTouch1.SetActive(true);
                DontTouch2.SetActive(true);
                this.gameObject.GetComponentInChildren<Text>().text = "Restart Tutorial";
                break;
            case 5:
                DontTouch1.SetActive(false);
                DontTouch2.SetActive(false);
                ThisIsYou.SetActive(true);
                this.gameObject.GetComponentInChildren<Text>().text = "Next";
                numClicked = -1;
                break;
        }
        numClicked++;
    }
}
