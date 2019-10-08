using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TransitionControllerAlan : MonoBehaviour
{
    public GameObject IntroVideoObject;
    private VideoClip introClip;
    private double clipDuration;
    public double totalTime;
    public double endingBuffer;
    public string level;
    //public string VideoClipName;

    // Start is called before the first frame update
    void Start()
    {
        VideoPlayer introVideoPlayer = IntroVideoObject.GetComponent<VideoPlayer>();
        //introVideoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, VideoClipName);
        introClip = introVideoPlayer.clip;
        clipDuration = introClip.length;
        totalTime = Time.time + clipDuration + endingBuffer;
        //introVideoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //clipDuration = introClip.length;
        //Debug.Log(clipDuration);
        if (Time.time >= totalTime)
        {

            //Debug.Log("HELLO");
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(level);
        }
    }
}
