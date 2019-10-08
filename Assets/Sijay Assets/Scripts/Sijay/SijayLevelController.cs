using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SijayLevelController : MonoBehaviour
{
    public GameObject player;
    public GameObject killWall1;
    public GameObject killWall2;
    public GameObject boss;
    private int phase = 0;
    public AudioSource mainMusic;
    public AudioSource bossMusic;
    public float crossFadeSpeed;
    private bool transition = false;
    public GameObject fade;
    private bool fading;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = fade.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartBossMusic", 13.3f);
    }

    void StartBossMusic() { transition = true; }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
        }
        if (fading)
        {
            sr.color = sr.color + new Color(0f,0f,0f,0.01f);
        }
        if (transition)
        {
            if (mainMusic.volume > 0f)
            {
                mainMusic.volume -= crossFadeSpeed;
            }
            if (bossMusic.volume < 1f)
            {
                bossMusic.volume += crossFadeSpeed;
            }
        }
    }

    void PlayerDied()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void PlayerWon()
    {
        fading = true;
        player.SendMessage("Invul");
        Invoke("NextLevel", 2.0f);
        GameObject.FindGameObjectWithTag("LevelUnlocker").GetComponent<LevelUnlock>().level3 = true;
    }

    void NextLevel()
    {
        SceneManager.LoadScene("BryantIntro");
    }

    void StartPhase1()
    {
        if (phase < 1)
        {
            phase = 1;

            killWall1.SendMessage("Activate");
        }
    }

    void StartPhase2()
    {
        if (phase < 2)
        {
            phase = 2;

            killWall2.SendMessage("Activate");
        }
    }

    void StartPhase3()
    {
        if (phase < 3)
        {
            phase = 3;

            boss.SendMessage("Activate");
        }
    }
}
