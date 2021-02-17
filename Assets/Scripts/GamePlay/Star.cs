using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public float rotationSpeed = 30;
    public GameObject explosionParticles;
    [SerializeField] GamePlayEventHubSO gamePlayEvents = default;


    void OnTriggerEnter(Collider other)
    {
        

        Destroy(other.gameObject);
        Destroy(this.gameObject, 0.3f);
        if (other.gameObject.CompareTag(GameTagsConfig.BallMainTag))
        {
            gamePlayEvents.BallWasted();
        }
        else { gamePlayEvents.BallScored(); }
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed);
    }
}
