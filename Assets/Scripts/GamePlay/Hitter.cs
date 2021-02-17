using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitter : MonoBehaviour
{
 
     private GameObject ball2hit = default; 
    [SerializeField] private float stickRotateSpeed =0f;
    [SerializeField] private InputEventHubSO inputEvents = default;
    [SerializeField] private GamePlayEventHubSO gamePlayEvents = default;

    public float power;
 

    Vector3 currentPosition;
    Quaternion currentRotation;
 
    Vector3 ballInitialPos;
    //private Rigidbody rb;

    Vector3 initialVector;

    void Start()
    {
       
        gamePlayEvents.Aiming();
        ball2hit = GameObject.Find("BallMain"); /*refactor */
        initialVector = transform.position - ball2hit.transform.position;

        ballInitialPos = ball2hit.transform.position;
        currentPosition = transform.position;
        currentRotation = transform.rotation;

        //rb = GetComponent<Rigidbody>();
        Predict();

        inputEvents.OnSlide += RotateStick;
        inputEvents.OnFirePressed += Shoot;
    }

     

    public Vector3 CalculateForce()
    {
        return transform.forward * power;
    }

    void Shoot()
    {
        if (gamePlayEvents.gpState == GamePlayEventHubSO.GamePlayState.Waiting) { return; }
        ball2hit.transform.SetParent(null);
         ball2hit.GetComponent<Rigidbody>().AddForce(CalculateForce(), ForceMode.Impulse);
         gamePlayEvents.BallReleased();

    }

    void  Update()
    {
        

        if (currentPosition != transform.transform.position)
        {
            Predict();
        }
        //Debug.Log("rb.rotation.y"+ rb.rotation.y);  

         
    }
 

    //private void RotateStick(float input)
    //{
    //    if (gamePlayEvents.gpState == GamePlayEventHubSO.GamePlayState.Waiting) { return; }
    //    if (Mathf.Abs(input) >= 0.2f)
    //    {

    //        //   transform.RotateAround(ball2hit.transform.position, Vector3.up, input * stickRotateSpeed *2f);
          
    //        RotateRigidBodyAroundPointBy(rb, ball2hit.transform.position, Vector3.up, input * stickRotateSpeed * Time.fixedDeltaTime  ); 
    //    } 
         
    //}

    void RotateStick(float input)
    {

        if (gamePlayEvents.gpState == GamePlayEventHubSO.GamePlayState.Waiting) { return; }
        if (Mathf.Abs(input) < 0.2) { return; }

        float rotateDegrees = 0f;
         
                rotateDegrees += input * stickRotateSpeed* Time.deltaTime;
             
            

            Vector3 currentVector = transform.position - ball2hit.transform.position;
            currentVector.y = 0;
            float angleBetween = Vector3.Angle(initialVector, currentVector) * (Vector3.Cross(initialVector, currentVector).y > 0 ? 1 : -1);
            float newAngle = Mathf.Clamp(angleBetween + rotateDegrees, -100, 100);
            rotateDegrees = newAngle - angleBetween;

            transform.RotateAround(ball2hit.transform.position, Vector3.up, rotateDegrees);
        


    }







    void Predict()
    {
        if (gamePlayEvents.gpState == GamePlayEventHubSO.GamePlayState.Waiting) { return; }

        gamePlayEvents.Prediction(ball2hit.transform.position, CalculateForce());
        //PredictionManager.instance.Predict(/*ballPrefab,*/ ball2hit.transform.position, CalculateForce());
    }

    //public void RotateRigidBodyAroundPointBy(Rigidbody rb, Vector3 origin, Vector3 axis, float angle)
    //{
    //    Quaternion desiredRot = Quaternion.AngleAxis(angle, axis);
    //   // Quaternion currentRot = rb.rotation;
    // //   Quaternion smootherRot = Quaternion.Lerp(currentRot, currentRot *desiredRot, 0.1f);

    //    rb.MovePosition(desiredRot * (rb.transform.position - origin) + origin);
    //    rb.MoveRotation(rb.transform.rotation * desiredRot);
    //}

     

    private void OnDestroy()
    {
        inputEvents.OnSlide -= RotateStick;
        inputEvents.OnFirePressed -= Shoot;
    }
}
