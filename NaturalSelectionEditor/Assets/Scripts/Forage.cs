using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forage : Behaviour
{

    [SerializeField] GameObject carrot;
    float eatTime;

    public override void StartBehaviour(){
        eatTime = 0f;
        NearestCarrot();
        myMotor.destination = carrot.transform.position;
    }

    public override void UpdateBehaviour(){
        if (carrot == null) {
            myAI.behaviourLocked = false;
            SuddenDecision();
            return;
        }

        

        float dist = (carrot.transform.position - transform.position).sqrMagnitude;
        if (dist <= .5f)
        {

            myAI.anim.PlayAnimation(BunnyAnimator.Animation.Eat);

            myAI.behaviourLocked = true;
            //EATING
            eatTime += Time.deltaTime;
            if (eatTime >= 2f)
            {
                //DONE EATING
                myAI.behaviourLocked = false;
                myAI.stats.satiation = Mathf.Clamp(myAI.stats.satiation + (1 - myAI.stats.satiation) * myAI.stats.foodConsumption, 0f, 1f);
                Destroy(carrot);
            }
        }
        else {
            myAI.anim.PlayAnimation(BunnyAnimator.Animation.Move);
        }
    }

    public override void EndBehaviour(){
        //STOP EATING
    }


    void NearestCarrot() {
        float dist = float.MaxValue;
        foreach (GameObject g in myAI.carrots) {
            float gDist = (g.transform.position - transform.position).sqrMagnitude;
            if (gDist < dist) {
                dist = gDist;
                carrot = g;
            }
        }
    }
}
