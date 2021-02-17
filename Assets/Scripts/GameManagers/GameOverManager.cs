using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] Button restartButton = default;
    [SerializeField] Button quitButton = default;
    private void Awake()
    {
        restartButton.onClick.AddListener(() =>
        {
            ApplicationManager.StartGame();
        });

        quitButton.onClick.AddListener(() =>
        {
            ApplicationManager.QuitGame();
        });


    }

}
