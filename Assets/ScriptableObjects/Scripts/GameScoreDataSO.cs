using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameScoreDataSO", menuName = "Game/GameScoreDataSO")]
public class GameScoreDataSO : ScriptableObject
{
    public int initialAttempts;
    [HideInInspector] public int attemptsLeft;

    public int initialLevel =1;
    [HideInInspector] public int currentLevel;


    private void OnEnable()
    {
        ResetLevel();
        ResetAttempts();
    }

    public void ResetAttempts()
    {
         
        attemptsLeft = initialAttempts; 
    }

    public void ResetLevel()
    {

        currentLevel = initialLevel;
    }


}
