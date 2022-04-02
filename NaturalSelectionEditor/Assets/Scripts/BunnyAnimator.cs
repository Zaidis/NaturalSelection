using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyAnimator : MonoBehaviour
{

    [SerializeField] BunnyMotor motor;
    public enum Animation { Move, Idle, Eat, Mate}
    Animation currentAnimation;

    [SerializeField] Transform artTransform;

    float t;

    void Start()
    {
        
    }

    void Update(){
        if (currentAnimation == Animation.Idle){
            artTransform.localPosition = new Vector3(0, 0, 0);
            return;
        }
        else if (currentAnimation == Animation.Move) {
            artTransform.LookAt(motor.destination, Vector3.up);
            artTransform.eulerAngles = new Vector3(0, artTransform.eulerAngles.y, 0);
            artTransform.localPosition = new Vector3(0, Mathf.Abs(Mathf.Sin(t)), 0);
        }
        else if (currentAnimation == Animation.Eat) {
            artTransform.eulerAngles = new Vector3(0, artTransform.eulerAngles.y, 25*Mathf.Sin(t));
            artTransform.localPosition = new Vector3(0, 0, 0);
        }
        else if (currentAnimation == Animation.Mate) {
            artTransform.localPosition = new Vector3(0, Mathf.Abs(.5f*Mathf.Sin(3*t)), Mathf.Abs(.5f * Mathf.Sin(3*t)));
        }


        t += Mathf.PI * Time.deltaTime * Random.Range(0.75f, 1.25f);
    }

    public void PlayAnimation(Animation anim) {
        if (currentAnimation != anim) {
            t = 0;
            currentAnimation = anim;
        }
        
    }
    
}
