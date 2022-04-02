using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : Behaviour
{

    public override void StartBehaviour() {
        myMotor.destination = transform.position;
    }

    public override void UpdateBehaviour(){
        //look cute
    }

    public override void EndBehaviour() {
        //need to do nothing
    }
}
