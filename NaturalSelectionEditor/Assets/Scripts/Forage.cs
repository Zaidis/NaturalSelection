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
            SuddenDecision();
            return;
        }

        float dist = (carrot.transform.position - transform.position).sqrMagnitude;
        if (dist <= .5f) {
            //EATING
            eatTime += Time.deltaTime;
            if (eatTime >= 1f) {
                //DONE EATING
                Destroy(carrot);
            }
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
