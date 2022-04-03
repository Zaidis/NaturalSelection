using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
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
    public float boostAddition;
    private float vert, hori;
    [SerializeField] Rigidbody rb;
    [SerializeField] Vector3 downwardForce;
    [SerializeField] ForceMode forceMode;
    [SerializeField] bool isTrailer;
    [SerializeField] bool canBoost;
    [SerializeField] float maxBoostMeter, boostConsumptionRate, boostRechargeRate, timeToBoostRecharge;
    [SerializeField] Image boostMeter;
    float currentBoostMeter;
    bool boosting;
    float timer = 0;
    private void Awake()
    {
        if (isTrailer)
            FindObjectOfType<PlayerInput>().actions["Move"].performed += InputMovement;
    }
    private void Start()
    {
        currentBoostMeter = maxBoostMeter;
        
        UpdateMeter();
        if(boostMeter.gameObject != null)
            boostMeter.gameObject.SetActive(canBoost);
    }
    private void Update()
    {
        if(!boosting && currentBoostMeter < maxBoostMeter)
        {
            timer += Time.deltaTime;
            if(timer > timeToBoostRecharge)
            {
                currentBoostMeter += boostRechargeRate * Time.deltaTime;
            }
        }
        else
        {
            timer = 0;
        }
        foreach (AxleInfo axleInfo in axleInfos)
        {
            ApplyLocalPositionToVisuals(axleInfo.leftWheel, axleInfo.leftTire);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel, axleInfo.rightTire);
        }
        UpdateMeter();
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
        rb.AddForce(downwardForce, forceMode);
       
            float motor = maxMotorTorque * vert;
            float steering = maxSteeringAngle * hori;
        if (canBoost && boosting && currentBoostMeter > 0)
        {
            motor += boostAddition;
            currentBoostMeter -= boostConsumptionRate * Time.fixedDeltaTime;
        }
            
            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (axleInfo.steering && !isTrailer)
                {
                    axleInfo.leftWheel.steerAngle = steering;
                    axleInfo.rightWheel.steerAngle = steering;
                }
                if (axleInfo.motor)
                {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;
                }
            }
        
    }
    void UpdateMeter()
    {
        if(boostMeter != null && boostMeter.gameObject.activeInHierarchy)
            boostMeter.fillAmount = currentBoostMeter / maxBoostMeter;
        
    }
    public void InputMovement(InputAction.CallbackContext callback)
    {
        vert = callback.ReadValue<Vector2>().y;
        hori = callback.ReadValue<Vector2>().x;

    }

    public void Boost(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            boosting = true;
        }
        else
        {
            boosting = false;
        }
    }

    public void ActivateBoostAbility()
    {
        canBoost = true;
        boostMeter.gameObject.SetActive(true);
    }
}