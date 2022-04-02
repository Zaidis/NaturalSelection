using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour : MonoBehaviour
{
    public BehaviourType behaviour;
    [SerializeField] protected BunnyMotor myMotor;
    [SerializeField] protected BunnyAI myAI;

    public virtual void StartBehaviour() {

    }

    public virtual void UpdateBehaviour() {

    }

    public virtual void EndBehaviour() {

    }

    public virtual void SuddenDecision() {
        myAI.DecideBehaviour();
    }

}
