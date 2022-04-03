using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BunnyAI : MonoBehaviour
{
    public BunnyStats stats;
    public BunnyAnimator anim;

    [SerializeField] GameObject earObjectW, earObjectB;
    [SerializeField] GameObject bigEarsW, bigEarsB;

    [SerializeField] GameObject bodyObject;
    [SerializeField] GameObject brownFur;

    [SerializeField] GameObject shoes, hat;

    public Behaviour[] behaviours;
    [SerializeField] Behaviour currentBehaviour;

    float behaviourCheckRate = 3f; //reduce with intelligence
    float t = 0f;

    //SENSES
    [SerializeField] LayerMask carrotMask, bunnyMask, truckMask;
    public List<GameObject> carrots = new List<GameObject>();
    public List<GameObject> bunnies = new List<GameObject>();

    public int timesMated = 0;
    public bool pregnant = false;
    float pregnancyTime = 0f;

    public BunnyStats mateStats;

    public bool adult = false;
    public float age = 0f;

    public bool behaviourLocked = false;

    void Start(){
        if (stats.fertality + stats.earSize + stats.speed == 0) {
            GeneticAlgorithm.instance.RandomizeBunny(gameObject);
        }
        BunnyManager.instance.bunnieStats.Add(stats);

        if (stats.earSize >= .75f) {
            earObjectW.SetActive(false);
            bigEarsW.SetActive(true);

            earObjectB.SetActive(false);
            bigEarsB.SetActive(true);
        }
        if (stats.fertality >= .75f) {
            bodyObject.SetActive(false);
            brownFur.SetActive(true);

            earObjectW.SetActive(false);
            bigEarsW.SetActive(false);
        }
        if (stats.speed >= .75f) {
            shoes.SetActive(true);
        }
        if (stats.intelligence >= .75f) {
            hat.SetActive(true);
        }
    }

    void Update(){
        t += Time.deltaTime;
        if (t >= behaviourCheckRate / (1+stats.intelligence)) {
            t = 0;
            DecideBehaviour(false);
        }

        age += Time.deltaTime * stats.growthRate;
        if (age > 30f) {
            adult = true;
        }

        transform.localScale = Vector3.one * Mathf.Clamp(age, 10f, 30f) / 30f;

        stats.satiation = Mathf.Clamp(stats.satiation - Time.deltaTime * 0.01f, 0f, 1f);

        if (pregnant) {
            pregnancyTime += Time.deltaTime;
            if (pregnancyTime >= 30f * stats.pregnancyDuration) { //*pregnancyduration
                //Debug.Log("Give Birth");
                GeneticAlgorithm.instance.GiveBirth(mateStats, stats, transform.position);
                pregnant = false;
                pregnancyTime = 0f;
            }
        }

        if (currentBehaviour != null) {
            currentBehaviour.UpdateBehaviour();
            //Debug.Log(currentBehaviour.behaviour);
        }

        
        
    }

    public void DecideBehaviour(bool refresh) {
        if (behaviourLocked) {
            return;
        }
        //GATHER INFO
        float detectionDistance = 100f * stats.earSize;
        //NERBY CARROTS
        DetectCarrots(detectionDistance);

        //NERBY MATES
        DetectBunnies(detectionDistance); //Only Ladies

        //TRUCK
        float truckDist2 = (BunnyManager.instance.truck.position - transform.position).sqrMagnitude;

        //MAKE DECISION
        float idleWeight = 1f * stats.lazyWeight;
        float forageWeight = 1f * stats.hungryWeight * (1 - stats.satiation) * carrots.Count;// stats.hungryWeight;
        float mateWeight = 1f * stats.hornyWeight * stats.satiation * bunnies.Count * stats.gender / (1f + timesMated);
        if (!adult) {
            mateWeight = 0f;
        }
        float fleeWeight = 1f * stats.scaredWeight * detectionDistance * detectionDistance / Mathf.Max(truckDist2, 1f);
        if (truckDist2 > detectionDistance * detectionDistance) {
            fleeWeight = 0;
        }
        
        float wanderWeight = 2f * stats.boredWeight;

        float total = idleWeight + forageWeight + mateWeight + fleeWeight + wanderWeight;

        float rand = Random.Range(0f, total);

        //ACTIVATE BEHAVIOUR
        if (rand <= idleWeight){
            SetBehaviour(BehaviourType.Idle, refresh);
        }
        else if (rand <= forageWeight + idleWeight) {
            SetBehaviour(BehaviourType.Forage, refresh);
        }else if (rand <= mateWeight + forageWeight + idleWeight) {
            SetBehaviour(BehaviourType.Mate, refresh);
        }else if (rand <= fleeWeight + mateWeight + forageWeight + idleWeight) {
            SetBehaviour(BehaviourType.Flee, refresh);
        }else if (rand <= wanderWeight + fleeWeight + mateWeight + forageWeight + idleWeight) {
            SetBehaviour(BehaviourType.Wander, refresh);
        }
        
    }

    public void SetBehaviour(BehaviourType type, bool refresh) {
        if (currentBehaviour != null && !refresh && type == currentBehaviour.behaviour) {
            return;
        }

        for (int i = 0; i < behaviours.Length; i++) {
            if (behaviours[i].behaviour == type) {
                if (currentBehaviour != null){
                    currentBehaviour.EndBehaviour();
                }
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
        if (stats.gender == 0) {
            return;
        }

        RaycastHit[] bunnyHits = Physics.SphereCastAll(transform.position, detectionDistance, Vector3.up, .01f, bunnyMask, QueryTriggerInteraction.Collide);
        GameObject[] bun = new GameObject[bunnyHits.Length];
        for (int i = 0; i < bunnyHits.Length; i++){
            bun[i] = bunnyHits[i].collider.gameObject;
        }
        bunnies.Clear();
        for (int i = 0; i < bun.Length; i++) {
            if (bun[i].GetComponent<BunnyStats>().gender == 0 && !bun[i].GetComponent<BunnyAI>().pregnant && bun[i].GetComponent<BunnyAI>().adult) {
                bunnies.Add(bun[i]);
            }
        }
    }

    public void Die() {
        BunnyManager.instance.bunnieStats.Remove(stats);
        //STUFF
        Destroy(gameObject);
    }
}
