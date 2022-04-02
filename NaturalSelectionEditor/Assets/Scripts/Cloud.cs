using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float deathTimer = 60;
    private void Update() { 
        //cloud movement
        transform.position += Vector3.forward * 0.05f;

        deathTimer -= Time.deltaTime;

        if(deathTimer <= 0) {
            Destroy(this.gameObject);
        }
    }

}
