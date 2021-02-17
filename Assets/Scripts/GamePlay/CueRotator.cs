using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueRotator : MonoBehaviour
{
    private float rotationSpeed = 20f;
    [SerializeField] private Transform point = default;

    Vector3 pos;
    private void Start()
    {
        pos = point.position;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        var aimTDir = pos - this.transform.position;
        float aimB = Vector3.Angle(aimTDir, Vector3.up);
        float testDump = Mathf.Lerp(horizontal * rotationSpeed, aimB, 0.1f );
      
      //  transform.Rotate(/*-vertical * rotationSpeed */ 0f, testDump * Time.deltaTime, 0.0f);
      //  transform.LookAt(point);
        transform.RotateAround(pos, Vector3.up, testDump * Time.deltaTime);
    }
}
