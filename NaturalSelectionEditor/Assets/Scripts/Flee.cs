using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : Behaviour
{

    public override void StartBehaviour(){
        myMotor.sprint = 5f;
    }

    public override void UpdateBehaviour(){
        Vector3 direction = BunnyManager.instance.truck.position - transform.position;
        direction.Normalize();
        myMotor.destination = transform.position - direction*100f;
        myAI.anim.PlayAnimation(BunnyAnimator.Animation.Move);
    }

    public override void EndBehaviour(){
        myMotor.sprint = 1f;
        //STOP EATING
    }
}
