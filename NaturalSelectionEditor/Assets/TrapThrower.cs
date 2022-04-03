using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TrapThrower : MonoBehaviour
{
    [SerializeField] float coolDown;
    float timer = 0;
    bool pressing;
    [SerializeField] float randomDirectionConstraint, randomVerticalConstraint;
    [SerializeField] GameObject trap;
    [SerializeField] Transform throwPoint;
    // Update is called once per frame
    void Update()
    {
        if(timer < coolDown)
        {
            timer += Time.deltaTime;
        }
        else if(pressing && timer >= coolDown)
        {
            timer = 0;
            //Throw Trap
            GameObject g = Instantiate(trap, throwPoint.position, Quaternion.identity);
            g.GetComponent<Rigidbody>().velocity = Vector3.up + new Vector3(Random.Range(-randomDirectionConstraint, randomDirectionConstraint), Random.Range(-randomVerticalConstraint, randomVerticalConstraint), Random.Range(-randomDirectionConstraint, randomDirectionConstraint));
        }
    }

    public void ThrowTrap(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            pressing = true;
        }else if (callbackContext.canceled)
        {
            pressing = false;
        }
    }
}
