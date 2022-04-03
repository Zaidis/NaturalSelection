using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
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

    public float speed;
    [SerializeField] TextMeshProUGUI speedometerText;
    Vector3 previousPos;
    float speedUpdateTimer = 0f;
    float engineUpdateTimer = 0f;

    [SerializeField] AudioClip idleLoop, fastLoop;
    [SerializeField] AudioSource engineAS, boostSource;


    private void Awake()
    {
        if (isTrailer)
            FindObjectOfType<PlayerInput>().actions["Move"].performed += InputMovement;
    }
    private void Start()
    {
        if (!isTrailer)
        {
            currentBoostMeter = maxBoostMeter;

            UpdateMeter();
            if (boostMeter.gameObject != null)
                boostMeter.gameObject.SetActive(canBoost);
        }
        previousPos = transform.position;
        
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
        UpdateSpeed();
        UpdateSFX();
    }

    void UpdateSpeed() {
        float trueSpeed = (transform.position - previousPos).magnitude * 2.2369356f / Time.deltaTime;
        speed = (speed*3 + trueSpeed) / 4f;

        previousPos = transform.position;
        if (speedometerText != null && speedUpdateTimer >= .5f) {
            speedometerText.text = Mathf.RoundToInt(speed).ToString();
            speedUpdateTimer = 0f;
        }
        speedUpdateTimer += Time.deltaTime;
    }

    void UpdateSFX() {
        if (engineAS != null) {
            if (boosting && engineUpdateTimer > .5f){
                engineAS.clip = fastLoop;
                if (!engineAS.isPlaying){
                    engineAS.Play();
                }
                engineUpdateTimer = 0f;
            }
            else if (engineUpdateTimer > .5f)
            {
                engineAS.clip = idleLoop;
                if (!engineAS.isPlaying){
                    engineAS.Play();
                }
                engineUpdateTimer = 0f;
                
            }

            engineAS.pitch = 1 + speed / 40f;

            engineUpdateTimer += Time.deltaTime;
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
        if(!isTrailer && boostMeter != null && boostMeter.gameObject.activeInHierarchy)
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
            boostSource.Play();
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