using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedPlatforms : MonoBehaviour
{
    //Imported Objects
    //public GameObject player;
    public GameObject fadingPlatform;
    public GameObject mainPlatform;


    //Timers
    private float mainTimer = 0f;
    private float fadeTimer = 0f;
    private float noneTimer = 0f;


    //CDs
    public float FadeCD;
    public float MainCD;
    public float NoneCD;

    //Explosion Properties
    private bool FadeDone;
    bool timer1Compl;
    bool mainFlag;
    bool fadeFlag;

    //Platform Timer Offset
    public float TimerOffset;

    // Start is called before the first frame update
    void Start()
    {
        //mainPlatform.SetActive(false);
        //fadingPlatform.SetActive(false);
        //MainPlat();
        mainFlag = true;
        fadeFlag = true;
        noneTimer = TimerOffset;
    }

    // Update is called once per frame
    void Update()
    {
        //if (FadeDone)
        //{
        //    MainPlat();
        //}
        //else if (mainTimer < Time.time)
        //{
        //    if (timer1Compl)
        //    {
        //        FadePlat();
        //        timer1Compl = false;
        //    }


        //    if (fadeTimer < Time.time)
        //    {
        //        FadeDone = true;
        //        fadingPlatform.SetActive(false);
        //    }


        //}

        if(noneTimer < Time.time && mainFlag && fadeFlag)
        {
            HideMain();
            mainFlag = false;
        }
        else if(mainTimer < Time.time && !mainFlag && fadeFlag)
        {
            HideFade();
            fadeFlag = false;
        }
        else if(fadeTimer < Time.time && !mainFlag && !fadeFlag)
        {
            RevealAll();
            mainFlag = true;
            fadeFlag = true;
        }
    }

    public void MainPlat()
    {
        mainPlatform.SetActive(true); //currently simulating turning on the explosion targetting animation
        mainTimer = Time.time + MainCD;
        FadeDone = false;
        timer1Compl = true;
    }

    public void FadePlat()
    {
        mainPlatform.SetActive(false);
        fadingPlatform.SetActive(true);
        fadeTimer = Time.time + FadeCD;
    }



    public void HideMain ()
    {
        mainPlatform.SetActive(false);
        mainTimer = Time.time + MainCD;
    }

    public void HideFade()
    {
        fadingPlatform.SetActive(false);
        fadeTimer = Time.time + FadeCD;
    }
    public void RevealAll()
    {
        mainPlatform.SetActive(true);
        fadingPlatform.SetActive(true);
        noneTimer = Time.time + NoneCD;
    }
}
