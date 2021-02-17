using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "InputEventHubSO", menuName = "Game/InputEventHubSO")]
public class InputEventHubSO : ScriptableObject
{
    //public event Action OnTapStarted;
    //public event Action OnTapEnded;
    public event Action<float> OnSlide;
    public event Action OnFirePressed;


    //public void TapStarted()
    //{
    //    OnTapStarted?.Invoke();
    //}

    public void Slide( float value)
    {
        OnSlide?.Invoke(value);
    }

    public void Fire( )
    {
        OnFirePressed?.Invoke();
    }

}

