using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyMotor : MonoBehaviour
{
    [SerializeField] LayerMask ground;
    public Vector2 destination;

    public float speed = 1f;

    void FixedUpdate(){
        Move();
    }

    void Move() {
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.z);
        if ((currentPos - destination).SqrMagnitude() > .25f) {
            Vector2 direction2 = destination - currentPos;
            direction2 = direction2.normalized * Time.deltaTime * speed;
            
            transform.position = new Vector3(currentPos.x + direction2.x, GetTerrainHeight(currentPos + direction2), currentPos.y + direction2.y);
            
        }
    }

    float GetTerrainHeight(Vector2 pos) {
        RaycastHit hit;
        Physics.Raycast(new Vector3(pos.x, 500, pos.y), Vector3.down, out hit, 1000f, ground, QueryTriggerInteraction.Ignore);
        //Debug.Log(hit.point.y);
        return hit.point.y;
    }


}
