using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forage : Behaviour
{
    public override void StartBehaviour(){
        myMotor.destination = Vector3.zero;
    }

    public override void UpdateBehaviour(){

    }

    public override void EndBehaviour(){
        //STOP EATING
    }
}
