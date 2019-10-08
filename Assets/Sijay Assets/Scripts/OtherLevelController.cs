using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherLevelController : MonoBehaviour
{
    public delegate void LevelEvent();
    public event LevelEvent OnLevelLoaded;
    public event LevelEvent OnLevelFailed;
    public event LevelEvent OnLevelCompleted;

    public void LevelLoad() { OnLevelLoaded?.Invoke(); }
    public void LevelFail() { OnLevelFailed?.Invoke(); lost = true; }
	public void LevelComplete() { OnLevelCompleted?.Invoke(); won = true; }

    public event LevelEvent OnStageOneCompletion;
    public event LevelEvent OnStageTwoCompletion;

    public void StageOneCompleted() { OnStageOneCompletion?.Invoke(); }
    public void StageTwoCompleted() { OnStageTwoCompletion?.Invoke(); }

    private bool stage2 = false;
    private bool stage3 = false;
    private bool won = false;
    private bool lost = false;

    void Start()
    {
        LevelLoad();
        OnStageOneCompletion += OnStageTwo;
        OnStageTwoCompletion += OnStageThree;
    }

    void OnStageTwo()
    {
        stage2 = true;
    }

    void OnStageThree()
    {
        stage3 = true;
    }

    void Update()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        int numTargets = targets.Length;
        if(numTargets == 0 && !lost)
        {
            if (stage3)
            {
                LevelComplete();
            }
            else if (stage2)
            {
                StageTwoCompleted();
            }
            else
            {
                StageOneCompleted();
            }
        }
        if(GameObject.FindGameObjectWithTag("Player") == null && !won)
        {
            LevelFail();
        }
    }
}
