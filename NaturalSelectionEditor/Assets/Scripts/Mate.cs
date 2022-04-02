using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mate : Behaviour
{
    [SerializeField] GameObject mate;
    BunnyAI mateAI;
    float mateTime;

    public override void StartBehaviour(){
        mateTime = 0f;
        BestMate();
        mateAI = mate.GetComponent<BunnyAI>();
        myAI.behaviourLocked = true;
        mateAI.SetBehaviour(BehaviourType.Idle, true);
        mateAI.behaviourLocked = true;
    }

    public override void UpdateBehaviour(){
        if (mate == null) {
            myAI.behaviourLocked = false;
            SuddenDecision();
            return;
        }

        

        myMotor.destination = mate.transform.position;
        float dist = (mate.transform.position - transform.position).sqrMagnitude;
        if (dist <= .5f)
        {

            myAI.anim.PlayAnimation(BunnyAnimator.Animation.Mate);

            //MATING
            mateTime += Time.deltaTime;
            if (mateTime >= 4f)
            {
                //DONE MATING
                //SPAWN BUNNIES
                //Debug.Log("Pregnancy!");
                myAI.timesMated++;
                mateAI.timesMated++;

                mateAI.pregnant = true;

                GeneticAlgorithm.instance.CopyStats(mateAI.mateStats, myAI.stats);

                //mateAI.mateStats = myAI.GetComponent<BunnyStats>();

                mateAI.behaviourLocked = false;
                myAI.behaviourLocked = false;
                SuddenDecision();

            }
        }
        else {
            myAI.anim.PlayAnimation(BunnyAnimator.Animation.Move);
        }
    }

    public override void EndBehaviour(){
        //STOP EATING
    }


    void BestMate() {
        float bestMate = -float.MaxValue;
        foreach (GameObject g in myAI.bunnies) {
            float score = 1f / ((g.transform.position - transform.position).sqrMagnitude * (1f + g.GetComponent<BunnyAI>().timesMated));
            if (score > bestMate) {
                bestMate = score;
                mate = g;
            }
        }
    }
}
