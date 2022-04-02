using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Behaviour
{
    Vector3 randomSpot;
    
    public override void StartBehaviour(){
        randomSpot = transform.position + new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
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
