using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameScoreDataSO scoreData = default;
    //[SerializeField] private GamePlayEventHubSO gamePlayEvents = default;

    [SerializeField] private Image filledStar = default;
    [SerializeField] Text attemptsText = default;
    [SerializeField] Text currentLevelText = default;

  


    private int attemptsLeft;
    private int maxAttempts;
    private int currentLevel;

    void Start()
    {    
        if (filledStar == null) { filledStar = transform.GetChild(0).GetComponent<Image>(); }
        maxAttempts = scoreData.initialAttempts;
        attemptsLeft = scoreData.attemptsLeft;
        currentLevel = scoreData.currentLevel;
        SetAttempts();
        SetLevel();

    }


    private void SetAttempts()
    {
        
        float value = (float)attemptsLeft / (float)maxAttempts;
        filledStar.fillAmount = value; 
        attemptsText.text = attemptsLeft.ToString();
    }
 

    private void SetLevel()
    {
        currentLevelText.text = "level "+scoreData.currentLevel.ToString();
    }

     

}
