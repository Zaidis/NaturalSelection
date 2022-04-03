using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyMotor : MonoBehaviour
{
    [SerializeField] LayerMask ground;
    public Vector3 destination;
    public float sprint = 1f;


    [SerializeField] BunnyStats myStats;

    private void Start()
    {
        destination = transform.position;
    }

    void FixedUpdate(){
        Move();
    }

    void Move() {
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 dest = new Vector2(destination.x, destination.z);
        if ((currentPos - dest).SqrMagnitude() > .25f) {
            Vector2 direction2 = dest - currentPos;
            direction2 = direction2.normalized * Time.deltaTime * 2f * myStats.speed * sprint;
            
            transform.position = new Vector3(currentPos.x + direction2.x, GetTerrainHeight(currentPos + direction2), currentPos.y + direction2.y);
            
        }
    }

    float GetTerrainHeight(Vector2 pos) {

        return Terrain.activeTerrain.SampleHeight(new Vector3(pos.x, 0, pos.y));
        /*
        RaycastHit hit;
        Physics.Raycast(new Vector3(pos.x, 500, pos.y), Vector3.down, out hit, 1000f, ground, QueryTriggerInteraction.Ignore);
        //Debug.Log(hit.point.y);
        return hit.point.y;
        */
    }


}
