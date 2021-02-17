using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class BallMain : MonoBehaviour
{

    [SerializeField] private GamePlayEventHubSO gamePlayEvents = default;
    Rigidbody rb;
    Collider col;
    float minAllowedVelocity = 1f;
    float velocityMagnitude;

    void Start()
    {
        gameObject.tag = GameTagsConfig.BallMainTag;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        gamePlayEvents.OnBallReleased += CheckVisibility;
        gamePlayEvents.OnBallReleased += CheckVelocity;

    }
 

    private void CheckVisibility()
    {
        StartCoroutine(nameof(IsVisibleToCamera));
        //InvokeRepeating(nameof(IsVisibleToCamera), 0.5f, 1f);
    }

    IEnumerator IsVisibleToCamera()
    {
        bool needCheck = true;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        while (needCheck)
        {
            if (!GeometryUtility.TestPlanesAABB(planes, col.bounds))
            {
                gamePlayEvents.BallWasted();
                //Debug.Log("IsVisibleToCamera "  );
                StopAllCoroutines();
            }
            yield return new WaitForSeconds(1f);
        }
        yield return null;


    }
     


   

    private void CheckVelocity()
    {

        //StartCoroutine(nameof(IsBallStopped));
        InvokeRepeating(nameof(IsBallStopped), 1.5f, 2f);
    }


    private void IsBallStopped()
    {
        if (rb.velocity.magnitude < minAllowedVelocity)
        {
           // Debug.Log("IsBallStopped");
            gamePlayEvents.BallWasted();
            StopAllCoroutines();
        }
    }


    //IEnumerator IsBallStopped()
    //{


    //    yield return new WaitForSeconds(0.5f);
    //    if (rb.velocity.magnitude <  minAllowedVelocity)
    //    {
    //        Debug.Log("IsBallStopped");
    //        //yield return new WaitForSeconds(2f);

    //        gamePlayEvents.BallWasted();
    //        //StopAllCoroutines();
    //    }

    //    yield return null;
    //}





    private void OnDestroy()
    {
        gamePlayEvents.OnBallReleased -= CheckVisibility;
        gamePlayEvents.OnBallReleased -= CheckVelocity;
    }
}
