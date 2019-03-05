using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public delegate void LevelEvent();
    public event LevelEvent OnLevelLoaded;
    public event LevelEvent OnLevelFailed;
    public event LevelEvent OnLevelCompleted;

    public void LevelLoad() { OnLevelLoaded?.Invoke(); }
    public void LevelFail() { OnLevelFailed?.Invoke(); }
	public void LevelComplete() { OnLevelCompleted?.Invoke(); }

    public event LevelEvent OnStageOneCompletion;
    public event LevelEvent OnStageTwoCompletion;
    public event LevelEvent OnStageThreeCompletion;

    public void StageOneCompleted() { OnStageOneCompletion?.Invoke(); }
    public void StageTwoCompleted() { OnStageTwoCompletion?.Invoke(); }
    public void StageThreeCompleted() { OnStageThreeCompletion?.Invoke(); }

    private bool stage2 = false;
    private bool stage3 = false;

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
        if(numTargets == 0)
        {
            if (stage3)
            {
                StageThreeCompleted();
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
    }
}
