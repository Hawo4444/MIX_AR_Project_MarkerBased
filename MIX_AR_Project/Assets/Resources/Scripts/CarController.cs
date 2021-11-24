using UnityStandardAssets.CrossPlatformInput;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class CarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private Rigidbody car;

    [SerializeField]
    private WheelCollider frontLeftWheelCollider;
    [SerializeField]
    private WheelCollider frontRightWheelCollider;
    [SerializeField]
    private WheelCollider rearLeftWheelCollider;
    [SerializeField]
    private WheelCollider rearRightWheelCollider;
    [SerializeField]
    private Transform frontLeftWheelTransform;
    [SerializeField]
    private Transform frontRightWheelTransform;
    [SerializeField]
    private Transform rearLeftWheelTransform;
    [SerializeField]
    private Transform rearRightWheelTransform;

    [SerializeField]
    private float maxSteeringAngle;
    [SerializeField]
    private float motorForce;
    [SerializeField]
    private float brakeForce;

    private void Start()
    {
        car = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        verticalInput = CrossPlatformInputManager.GetAxis("Vertical");
    }

    private void HandleSteering()
    {
        steerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    private void HandleMotor()
    {
        var carVelocity = transform.InverseTransformDirection(car.velocity).z;

        if (verticalInput < 0 && carVelocity > 0)
        {
            ApplyBrakeTorque(brakeForce);
        }
        else
        {
            if (frontLeftWheelCollider.brakeTorque > 0)
            {
                ApplyBrakeTorque(0);
            }
        }

        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
    }

    private void UpdateWheels()
    {
        UpdateWheelPose(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPose(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPose(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPose(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateWheelPose(WheelCollider wheelCollider, Transform trans)
    {
        wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
        trans.rotation = rot;
        trans.position = pos;
    }

    private void ApplyBrakeTorque(float torque)
    {
        frontLeftWheelCollider.brakeTorque = torque;
        frontRightWheelCollider.brakeTorque = torque;
        rearLeftWheelCollider.brakeTorque = torque;
        rearRightWheelCollider.brakeTorque = torque;
    }
}