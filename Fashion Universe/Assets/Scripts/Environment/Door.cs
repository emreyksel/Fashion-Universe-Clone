using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public HingeJoint hinge;
    JointMotor motor;
    public float velocity;
    public float angle;

    private void Start()
    {
        hinge = GetComponent<HingeJoint>();
        motor = hinge.motor;
    }

    private void Update()
    {
        angle = hinge.angle;
        motor.targetVelocity = -angle;
        hinge.motor = motor;
    }
}
