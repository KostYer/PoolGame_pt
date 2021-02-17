using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PredictionManager : MonoBehaviour///Singleton<PredictionManager>
{
    public GameObject obstacles;
    public GameObject ballMain;
    public int maxIterations;

    [SerializeField] private float distanceToBall;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Texture lineTexture;

    [SerializeField] private GamePlayEventHubSO gamePlayEvents = default;


    Scene currentScene;
    Scene predictionScene;

    PhysicsScene currentPhysicsScene;
    PhysicsScene predictionPhysicsScene;

    List<GameObject> dummyObstacles = new List<GameObject>();

    [HideInInspector] public LineRenderer lineRenderer;
    GameObject dummy;
  
   

    private void Awake()
    {
        FakeBall.fakeBallPool = new Pooler(ballMain, maxIterations);
        currentScene = SceneManager.GetActiveScene();
        currentPhysicsScene = currentScene.GetPhysicsScene();
        CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        predictionScene = SceneManager.CreateScene("Prediction", parameters);
        predictionPhysicsScene = predictionScene.GetPhysicsScene();
        Physics.autoSimulation = false;
        CopyAllObstacles();
    }

    void Start()
    {
        
        lineRenderer = GetComponent<LineRenderer>();

        gamePlayEvents.OnAiming += EnableLineRenderer;
        gamePlayEvents.OnBallReleased += DisableLineRenderer;
        gamePlayEvents.OnPrediction += Predict;
    }

    void FixedUpdate()
    {
        if (currentPhysicsScene.IsValid())
        {
            currentPhysicsScene.Simulate(Time.fixedDeltaTime);
        }
    }


  
    public void CopyAllObstacles()
    {
        foreach (Transform t in obstacles.transform)
        {
            if (t.gameObject.GetComponent<Collider>() != null)
            {
                GameObject fakeT = Instantiate(t.gameObject);
                fakeT.transform.position = t.position;
                fakeT.transform.rotation = t.rotation;
                Renderer fakeR = fakeT.GetComponent<Renderer>();
                if (fakeR)
                {
                    fakeR.enabled = false;
                }
                SceneManager.MoveGameObjectToScene(fakeT, predictionScene);
                dummyObstacles.Add(fakeT);
            }
        }
    }

 


     

    void KillAllObstacles()
    {
        foreach (var o in dummyObstacles)
        {
            Destroy(o);
        }
        dummyObstacles.Clear();
    }

    public void Predict(/*GameObject subject,*/ Vector3 currentPosition, Vector3 force)
    {
        bool isInterrupted = false;
        if (currentPhysicsScene.IsValid() && predictionPhysicsScene.IsValid())
        {
            if (dummy == null)
            {
                //dummy = Instantiate(ballMain);
                dummy = FakeBall.fakeBallPool.Get();
                SceneManager.MoveGameObjectToScene(dummy, predictionScene);
            }

            dummy.transform.position = currentPosition;
            
            //dummy.GetComponent<Rigidbody>().WakeUp();
            dummy.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            if (lineRenderer == null) { lineRenderer = GetComponent<LineRenderer>(); }
            lineRenderer.positionCount = maxIterations;

            Vector3[] positionsForLR = new  Vector3 [maxIterations];
            Vector3[] positionsForLRinterrupted;
            for (int i = 0; i < maxIterations; i++)
            {

                positionsForLR[i] = dummy.transform.position;
                predictionPhysicsScene.Simulate(Time.fixedDeltaTime);
                //if (Physics.SphereCast(dummy.transform.position, 0.1f,  dummy.transform.forward, out var hit, 100f, layer))
                if (IsOverlapSphere (dummy.transform.position, 0.5f, layer))
                {
                    
                    positionsForLRinterrupted = new Vector3[i];
                    Array.Copy(positionsForLR, positionsForLRinterrupted, i);
                    lineRenderer.positionCount = i;
                    lineRenderer.SetPositions(positionsForLRinterrupted);
                    isInterrupted = true;
                    //return;  //weird behaviour
                    break;
                }
            }

            if (!isInterrupted) { lineRenderer.SetPositions(positionsForLR); }


            lineRenderer.Simplify(0.5f); //smoothing the line

            //dummy.GetComponent<Rigidbody>().velocity = Vector3.zero; ///didnt help
            //dummy.GetComponent<Rigidbody>().Sleep();///didnt help
            //SceneManager.MoveGameObjectToScene(dummy, currentScene); //didnt help
            //FakeBall.fakeBallPool.Free(dummy);
 
            Destroy(dummy);
        }
    }

    void OnDestroy()
    {
        KillAllObstacles();
    }

    private bool IsOverlapSphere(Vector3 pos, float radius, LayerMask layer)
    {
        Collider[] col = Physics.OverlapSphere(pos, radius, layer);

        return col.Length > 0;

    }


    private void EnableLineRenderer()
    {

        this.lineRenderer.enabled = true;
    }

    private void DisableLineRenderer()
    {

        this.lineRenderer.enabled = false;
    }

    private void OnDisable()
    {
        gamePlayEvents.OnAiming -= EnableLineRenderer;
        gamePlayEvents.OnBallReleased -= DisableLineRenderer;
        gamePlayEvents.OnPrediction -= Predict;
    }

}


 