using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public Transform leftTire, rightTire;
    public bool motor;
    public bool steering;
}

public class CarMovement : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    private float vert, hori;


    private void Update()
    {
        foreach (AxleInfo axleInfo in axleInfos)
        {
            ApplyLocalPositionToVisuals(axleInfo.leftWheel, axleInfo.leftTire);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel, axleInfo.rightTire);
        }
    }
    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider, Transform wheel)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        wheel.transform.position = position;
        wheel.transform.rotation = rotation;
    }

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * vert;
        float steering = maxSteeringAngle * hori;

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            //ApplyLocalPositionToVisuals(axleInfo.leftWheel, axleInfo.leftTire);
            //ApplyLocalPositionToVisuals(axleInfo.rightWheel, axleInfo.rightTire);
        }
    }
    public void InputMovement(InputAction.CallbackContext callback)
    {
        vert = callback.ReadValue<Vector2>().y;
        hori = callback.ReadValue<Vector2>().x;

    }
}