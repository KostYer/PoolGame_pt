using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour///Singleton<LevelManager>
{
    public int maxScore;
    public int balls;
    public int maxObstacles;

    [SerializeField] private GameScoreDataSO scoreData = default;
    [SerializeField] private GamePlayEventHubSO gamePlayEvents = default;

    int currentShoots;


  

    void Start()
    {
       

       currentShoots = scoreData.attemptsLeft;

        gamePlayEvents.OnBallScored += LevelBeated;
        gamePlayEvents.OnBallWasted += DecreaseAttempts;
        
    }

    private void DecreaseAttempts()
    {
        currentShoots--;
        scoreData.attemptsLeft--;
        if (currentShoots <= 0)
        {
            Lose();
        }
        else {

            StartCoroutine(nameof(ReloadScene));


        }
    }


    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(1.5f);
        ApplicationManager.ReloadCurrentScene();
    }
    
   
   
    void LevelBeated()
    {
        scoreData.ResetAttempts();
        scoreData.currentLevel++;
        StartCoroutine(NextLevelLoad());
    }

    void Lose()
    {
        StartCoroutine(waitAndCheckForReload());
    }

    IEnumerator NextLevelLoad()
    {
        yield return new WaitForSeconds(1);
        ApplicationManager.LoadNextGamePlayScene();

    }

    IEnumerator waitAndCheckForReload()
    {
        yield return new WaitForSeconds(0.3f);
        ApplicationManager.GameOver();

    }

    private void OnDestroy()
    {
        gamePlayEvents.OnBallScored -= LevelBeated;
        gamePlayEvents.OnBallWasted -= DecreaseAttempts;
    }


}
