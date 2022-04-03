using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class FlipDetection : MonoBehaviour
{
    [SerializeField] GameObject text;
    [SerializeField] Transform truck;
    public bool canRespawn;
    float timer = 0;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            timer += Time.deltaTime;
            if(timer > 3)
            {
                text.SetActive(true);
                canRespawn = true;
                timer = 0;
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            timer = 0;
        }
    }
    public void Respawn(InputAction.CallbackContext callback)
    {
        if (canRespawn && callback.performed)
        {
            canRespawn = false;
            text.SetActive(false);
            truck.eulerAngles = new Vector3(truck.rotation.x, truck.rotation.y, 0);
        }
    }
}
