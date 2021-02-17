using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


[CreateAssetMenu(fileName = "GamePlayEventHub", menuName = "Game/GamePlayEventHub")]
public class GamePlayEventHubSO : ScriptableObject
{
    public event Action OnBallScored;
    public event Action OnBallWasted;
    public event Action OnBallReleased;
    public event Action OnAiming;
    public event Action OnLevelReload;
    public event Action<Vector3, Vector3> OnPrediction;



    public GamePlayState gpState = GamePlayState.Aiming;

    public void BallScored()
    {
        OnBallScored?.Invoke();

    }

    public void BallWasted()
    {
        OnBallWasted?.Invoke();
    }

    public void BallReleased()
    {
        OnBallReleased?.Invoke();
        gpState = GamePlayState.Waiting;
    }

    public void Aiming()
    {
        OnAiming?.Invoke();
        gpState = GamePlayState.Aiming;
    }


    public void LevelReload()
    {
        OnLevelReload?.Invoke();
    }

    public void Prediction(Vector3 pos, Vector3 force)
    {
        OnPrediction?.Invoke(pos, force);

    }

    public enum GamePlayState
    { 
        Aiming, 
        Waiting
    
    }
}
