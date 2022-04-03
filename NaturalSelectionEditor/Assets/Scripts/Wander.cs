using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Behaviour
{
    Vector3 randomSpot;
    
    public override void StartBehaviour(){
        randomSpot = new Vector3(30 +Random.Range(Terrain.activeTerrain.transform.position.x, Terrain.activeTerrain.transform.position.x + Terrain.activeTerrain.terrainData.size.x - 30),
            0,
            Random.Range(30 + Terrain.activeTerrain.transform.position.y, Terrain.activeTerrain.transform.position.y + Terrain.activeTerrain.terrainData.size.y - 30));
        myMotor.destination = randomSpot;
    }

    public override void UpdateBehaviour(){

        float dist = (randomSpot - transform.position).sqrMagnitude;
        if (dist <= .5f)
        {

            myAI.anim.PlayAnimation(BunnyAnimator.Animation.Idle);

            SuddenDecision();
            return;
        }
        else{
            myAI.anim.PlayAnimation(BunnyAnimator.Animation.Move);
        }
    }

    public override void EndBehaviour()
    {
        //STOP EATING
    }
}
