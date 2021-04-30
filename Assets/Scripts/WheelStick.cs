using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelStick : MonoBehaviour
{
    public Vector3 centerOfMass;
    private Rigidbody myRigidBody;
    void Start()
    {
        myRigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidBody.centerOfMass = centerOfMass;
    }
}
