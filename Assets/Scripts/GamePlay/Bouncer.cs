using UnityEngine;

public class Bouncer : MonoBehaviour{
    public float power;

    void OnCollisionEnter(Collision collision){
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 dir = collision.contacts[0].normal;
        if(rb != null){
            var currentVelocity =  rb.velocity.z ;
            rb.AddForce(dir * currentVelocity * power, ForceMode.Impulse);
        }
    }
}
