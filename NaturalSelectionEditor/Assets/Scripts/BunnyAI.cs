using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BunnyAI : MonoBehaviour
{

    public Behaviour[] behaviours;
    Behaviour currentBehaviour;

    float behaviourCheckRate = 2f; //reduce with intelligence
    float t = 0f;

    //SENSES
    [SerializeField] LayerMask carrotMask, bunnyMask, truckMask;
    public List<GameObject> carrots = new List<GameObject>();
    public List<GameObject> bunnies = new List<GameObject>();

    void Start()
    {
        
    }

    void Update(){
        t += Time.deltaTime;
        if (t >= behaviourCheckRate) {
            DecideBehaviour();
        }

        currentBehaviour.UpdateBehaviour();
    }

    public void DecideBehaviour() {
        //GATHER INFO
        float detectionDistance = 10f;
        //NERBY CARROTS
        DetectCarrots(detectionDistance);

        //NERBY MATES
        DetectBunnies(detectionDistance);

        //TRUCK
        

        //MAKE DECISION
        float idleWeight = 1f;
        float forageWeight = 1f;

        float total = idleWeight + forageWeight;

        float rand = Random.Range(0f, total);

        if (rand <= idleWeight){
            SetBehaviour(BehaviourType.Idle);
        }
        else if (rand <= forageWeight + idleWeight) {
            SetBehaviour(BehaviourType.Forage);
        }
        //ACTIVATE BEHAVIOUR
    }

    void SetBehaviour(BehaviourType type) {
        if (type == currentBehaviour.behaviour) {
            return;
        }

        for (int i = 0; i < behaviours.Length; i++) {
            if (behaviours[i].behaviour == type) {
                currentBehaviour.EndBehaviour();
                currentBehaviour = behaviours[i];
                currentBehaviour.StartBehaviour();
                return;
            }
        }
    }

    void DetectCarrots(float detectionDistance) {
        RaycastHit[] carrotHits = Physics.SphereCastAll(transform.position, detectionDistance, Vector3.up, .01f, carrotMask, QueryTriggerInteraction.Collide);
        GameObject[] car = new GameObject[carrotHits.Length];
        for (int i = 0; i < carrotHits.Length; i++)
        {
            car[i] = carrotHits[i].collider.gameObject;
        }
        carrots = car.ToList();
    }

    void DetectBunnies(float detectionDistance) {
        RaycastHit[] bunnyHits = Physics.SphereCastAll(transform.position, detectionDistance, Vector3.up, .01f, carrotMask, QueryTriggerInteraction.Collide);
        GameObject[] bun = new GameObject[bunnyHits.Length];
        for (int i = 0; i < bunnyHits.Length; i++)
        {
            bun[i] = bunnyHits[i].collider.gameObject;
        }
        bunnies = bun.ToList();
    }
}
