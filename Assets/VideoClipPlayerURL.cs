using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoClipPlayerURL : MonoBehaviour
{
    public string VideoClipName;
    private VideoPlayer videoPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = this.GetComponent<UnityEngine.Video.VideoPlayer>();

        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, VideoClipName);

        videoPlayer.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
