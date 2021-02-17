using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Button startGameButton = default;
    [SerializeField] Button quitGameButton = default;
    void Awake()
    {
        startGameButton.onClick.AddListener(() =>
        {
            ApplicationManager.StartGame();
        });

        quitGameButton.onClick.AddListener(() =>
        {
            ApplicationManager.QuitGame();
        });


    }

    
}
