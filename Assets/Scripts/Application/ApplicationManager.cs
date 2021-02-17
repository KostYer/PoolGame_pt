using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 
    public static class ApplicationManager
    {
        static ApplicationManager()
        {
            //SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public static void OpenMainMenu()
        {

        SceneManager.LoadSceneAsync(ScenesConfig.MeinMenuScene, LoadSceneMode.Additive);
        Time.timeScale = 0f;

         }

  
    public static void StartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == ScenesConfig.FirstGameLevel) { return; }
        var unloadOperation= SceneManager.UnloadSceneAsync(currentScene.name);
        var leve01Scene = SceneManager.GetSceneByName(ScenesConfig.FirstGameLevel);
        SceneManager.LoadSceneAsync(ScenesConfig.FirstGameLevel);
        Time.timeScale = 1f;
    
    }
    public static void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(currentScene.name);
        SceneManager.LoadSceneAsync(currentScene.name);
    }

        public static void LoadNextGamePlayScene()
    {
        /*refactor : add exceptions*/
        var currentScene = SceneManager.GetActiveScene();
         var currentIndex = KeyByValue(ScenesConfig.gameLevels, currentScene.name.ToString());
        
        SceneManager.UnloadSceneAsync(currentScene);
        
        SceneManager.LoadSceneAsync(ScenesConfig.gameLevels[currentIndex+1]);

       

    }


    public static void QuitGame()
        {
            Application.Quit();
            Debug.Log("quit game");
        }



    public static void GameOver()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == ScenesConfig.GameOverScene) { return; }
        var unloadOperation = SceneManager.UnloadSceneAsync(currentScene.name);

        SceneManager.LoadSceneAsync(ScenesConfig.GameOverScene);
        Time.timeScale = 1f;

    }





        /*refactor :move to util*/
        public static T KeyByValue<T, W>(this Dictionary<T, W> dict, W val)
    {
        T key = default;
        foreach (KeyValuePair<T, W> pair in dict)
        {
            if (EqualityComparer<W>.Default.Equals(pair.Value, val))
            {
                key = pair.Key;
                break;
            }
        }
        return key;
    }


}
 