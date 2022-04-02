using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class DirtParticleController : MonoBehaviour
{
    VisualEffect dirt;
    WheelCollider wheel;
    [SerializeField] float multiplier;
    [SerializeField] CarMovement carMovement;
    private void Awake()
    {
        dirt = GetComponent<VisualEffect>();
        wheel = GetComponent<WheelCollider>();
        
        //print(dirt.GetFloat("Spawn Rate"));
    }
    
    // Update is called once per frame
    void Update()
    {
        if(wheel.isGrounded)
            dirt.SetFloat("Spawn Rate", (wheel.rpm * multiplier) / carMovement.maxMotorTorque);
        else
            dirt.SetFloat("Spawn Rate", 0);
    }
}
