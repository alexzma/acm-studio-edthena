using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlanLevelController : MonoBehaviour
{
    //Imported Controller
    public SpriteControllerv2 playerInfo;


    //Imported Objects
    public GameObject stationaryExpl;
    public GameObject trailingExpl;
    public GameObject bossExpl;
    public GameObject HeartDoor;

    //public delegate void LevelEvent();
    //public event LevelEvent OnLevelLoaded;
    //public event LevelEvent OnLevelFailed;
    //public event LevelEvent OnLevelCompleted;

    //public void LevelLoad() { OnLevelLoaded?.Invoke(); }
    //public void LevelFail() { OnLevelFailed?.Invoke(); lost = true; }
    //public void LevelComplete() { OnLevelCompleted?.Invoke(); won = true; }

    //public event LevelEvent OnStageOneCompletion;
    //public event LevelEvent OnStageTwoCompletion;

    //public void StageOneCompleted() { OnStageOneCompletion?.Invoke(); }
    //public void StageTwoCompleted() { OnStageTwoCompletion?.Invoke(); }

    //private bool stage2 = false;
    //private bool stage3 = false;
    private bool won = false;
    private bool lost = false;

    void Start()
    {
        //LevelLoad();
        //OnStageOneCompletion += OnStageTwo;
        //OnStageTwoCompletion += OnStageThree;
    }

    //void OnStageTwo()
    //{
    //    stage2 = true;
    //}

    //void OnStageThree()
    //{
    //    stage3 = true;
    //}

    void Update()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Objective");
        int numTargets = targets.Length;
        if (numTargets == 0 && !lost)
        {
            //Open the final boss wall and remove the explosions
            stationaryExpl.SetActive(false);
            trailingExpl.SetActive(false);
            bossExpl.SetActive(false);
            HeartDoor.SetActive(false);
        }

        if(playerInfo.HeartDestroyed)
        {
            GameObject.FindGameObjectWithTag("LevelUnlocker").GetComponent<LevelUnlock>().level3 = true;
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("Alan's Level Complete");
        }

        if (playerInfo.currentAnimState == 4 && !won)
        {
            if(playerInfo.deathAnimCompl)
            {

                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("Alan's Level Reset");
            }
            //LevelFail();
        }
    }
}
