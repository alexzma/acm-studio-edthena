using UnityEngine;

public class Music : MonoBehaviour
{
    AudioSource source;
    public AudioClip loop;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!source.isPlaying)
        {
            source.clip = loop;
            source.loop = true;
            source.Play();
        }
    }
}
