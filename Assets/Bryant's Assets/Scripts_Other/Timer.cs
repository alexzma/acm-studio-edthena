using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public SpriteControllerBryant sc;
    float timer;
    public Text txt;

    private void Start()
    {
        timer = sc.levelTime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        updateTimer();
    }

    void updateTimer()
    {
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        string niceTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        txt.text = niceTime;
    }
}
