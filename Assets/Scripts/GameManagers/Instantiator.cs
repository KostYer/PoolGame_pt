using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject pr_stick = default;
    [SerializeField] private GameObject pr_ballMain = default;
    [SerializeField] private GameObject pr_ball = default;
    [SerializeField] private GameObject pr_star = default;

    [Header("SpawnPoints")]
    [SerializeField] private Transform stickSpawnPoint = default;
    [SerializeField] private Transform ballMainSpawnPoint = default;
    [SerializeField] private Transform ballsSpawnPoints = default;
    [SerializeField] private Transform starSpawnPoint = default;

    [Space]
    [SerializeField] Transform holder = default;

    private List<GameObject> gamePlayEntities;

 

    void Awake()
    {
       
        gamePlayEntities = new List<GameObject>();
        InstantiateEntities();

    }

   
    private void InstantiateEntities()
    {
        gamePlayEntities.Clear();

        var stick = GameObject.Instantiate(pr_stick, stickSpawnPoint.position, stickSpawnPoint.transform.rotation);
        stick.transform.SetParent(holder);
        gamePlayEntities.Add(stick);

        var ballMain = GameObject.Instantiate(pr_ballMain, ballMainSpawnPoint.position, Quaternion.identity);
        ballMain.name = "BallMain";
        ballMain.transform.SetParent(holder);
        gamePlayEntities.Add(ballMain);

        var star = GameObject.Instantiate(pr_star, starSpawnPoint.position, Quaternion.identity);
        star.transform.SetParent(holder);
        gamePlayEntities.Add(star);

        foreach (Transform t in ballsSpawnPoints)
        {
            var ball = GameObject.Instantiate(pr_ball, t.position, Quaternion.identity);
            ball.transform.SetParent(holder);
            gamePlayEntities.Add(ball);
        }

    }
    private void KillGamePlayEntities()
    {
        if (gamePlayEntities.Count > 0)
        {
            foreach (var go in gamePlayEntities)
            {

                Destroy(go);
            }
        }

    }
 


}
