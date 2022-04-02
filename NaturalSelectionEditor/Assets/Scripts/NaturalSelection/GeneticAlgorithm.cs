using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GeneticAlgorithm : MonoBehaviour
{
    //Smaller value = bigger mutations
    public static GeneticAlgorithm instance;
    private void Awake()
    {
        if (instance == null){
            instance = this;
        }
        else {
            Destroy(this);
        }
        
    }


    public float mutationPercentage;
    public float mutateOverhaulChance;
    public GameObject bunnyPrefab;

    private void Start() {
        //CreateRandomBunny();
    }

    public GameObject CreateRandomBunny(Vector3 position) {
        //make baby
        GameObject bunny = Instantiate(bunnyPrefab, position, Quaternion.identity);

        BunnyStats stats = bunny.GetComponent<BunnyStats>();
        stats.fertality = RandomPercentage();
        stats.speed = RandomPercentage();
        stats.intelligence = RandomPercentage();
        stats.earSize = RandomPercentage();
        stats.growthRate = RandomPercentage();
        stats.foodConsumption = RandomPercentage();
        stats.pregnancyDuration = RandomPercentage();
        stats.mutationRate = RandomPercentage();

        BunnyAI bunnyAI = bunny.GetComponent<BunnyAI>();
        bunnyAI.age = 30f;
        bunnyAI.adult = true;

        return bunny;
    }

    /// <summary>
    /// Allows for a selected bunny to get new random stats. 
    /// </summary>
    /// <param name="bunny"></param>
    public void RandomizeBunny(GameObject bunny) {
        
        BunnyStats stats = bunny.GetComponent<BunnyStats>();
        stats.fertality = RandomPercentage();
        stats.speed = RandomPercentage();
        stats.intelligence = RandomPercentage();
        stats.earSize = RandomPercentage();
        stats.growthRate = RandomPercentage();
        stats.foodConsumption = RandomPercentage();
        stats.pregnancyDuration = RandomPercentage();
        stats.mutationRate = RandomPercentage();
    }

    /// <summary>
    /// Called when a male and female bunny give birth. 
    /// </summary>
    /// <param name="male">Male Parent</param>
    /// <param name="female">Female Parent</param>
    public void GiveBirth(BunnyStats male, BunnyStats female, Vector3 position){

        float femaleRate = female.fertality * female.foodConsumption;
        float maleRate = male.fertality * male.foodConsumption;
        float birthRate = femaleRate * maleRate;
        
        //most babies = 12 for the average female bunny
        for(int i = 0; i < 12; i++){
            if(DiceRoll(0, 101, birthRate)){
                //you make the baby
                CreateBunny(male, female, position);
            }
        }   
    }
    public void CreateBunny(BunnyStats dad, BunnyStats mom, Vector3 position)
    {
        //make baby
        GameObject bunny = Instantiate(bunnyPrefab, position, Quaternion.identity);
        BunnyStats child = bunny.GetComponent<BunnyStats>();

        float m = dad.mutationRate * mom.mutationRate * 100;
        MutationMaster(child, dad, mom, m);
        
    }

    /// <summary>
    /// The main function for mutating a new child. It decides whether or not the child will take
    /// it's parents genes, or perhaps a percentage of their genes. 
    /// </summary>
    /// <param name="child">The new bunny</param>
    /// <param name="dad">Male Parent</param>
    /// <param name="mom">Female Parent</param>
    /// <param name="m">Mutation Rate</param>
    private void MutationMaster(BunnyStats child, BunnyStats dad, BunnyStats mom, float m){
        //fertility
        if(DiceRoll(0, 101, m)){ //bunny is mutated
            child.fertality = MutateFloat(dad.fertality, mom.fertality, m);
        } else {
            child.fertality = mom.fertality;
            
        }
        //earSize
        if(DiceRoll(0, 101, m)){ //bunny is mutated
            child.earSize = MutateFloat(dad.earSize, mom.earSize, m);
        } else {
            child.earSize = mom.earSize;
        }
        //pregnancy duration
        if(DiceRoll(0, 101, m)){
            child.pregnancyDuration = MutateFloat(dad.pregnancyDuration, mom.pregnancyDuration, m);
        } else {
            child.intelligence = mom.intelligence;
            
        }
        //growth rate
        if (DiceRoll(0, 101, m)) {
            child.growthRate = MutateFloat(dad.growthRate, mom.growthRate, m);
        }
        else {
            child.growthRate = dad.growthRate;
        }
        //food consumption
        if (DiceRoll(0, 101, m)){
            child.foodConsumption = MutateFloat(dad.foodConsumption, mom.foodConsumption, m);
        } else {
            child.foodConsumption = dad.foodConsumption;
        }

        if(DiceRoll(0, 101, m)){
            child.jumpHeight = MutateFloat(dad.jumpHeight, mom.jumpHeight, m);
        } else {
            child.jumpHeight = dad.jumpHeight;
        }
        //speed
        if(DiceRoll(0, 101, m)){
            child.speed = MutateFloat(dad.speed, mom.speed, m);
        } else {
            if(DiceRoll(0, 101)){
                child.speed = dad.speed;
            } else {
                child.speed = mom.speed;
            }
        }
        //intelligence
        if(DiceRoll(0, 101, m)){
            child.intelligence = MutateFloat(dad.intelligence, mom.intelligence, m);
        } else {
            if(DiceRoll(0, 101)){
                child.intelligence = dad.intelligence;
            } else {
                child.intelligence = mom.intelligence;
            }
        }
        //mutation rate
        if(DiceRoll(0, 101, m)){
            child.mutationRate = MutateFloat(dad.mutationRate, mom.mutationRate, m);
        } else {
            if(DiceRoll(0, 101)){
                child.mutationRate = dad.mutationRate;
            } else {
                child.mutationRate = mom.mutationRate;
            }
        }
    }
    private float MutateFloat(float dadValue, float momValue, float mutationRate){
        if(DiceRoll(0, 101, mutateOverhaulChance)){
            return (float)UnityEngine.Random.Range(0f,0.99f);
        }
        mutationRate /= mutationPercentage; 
        float rand = UnityEngine.Random.Range(dadValue, momValue);
        float newValue = 0;
        if(DiceRoll(0, 101)){
            newValue = rand + mutationRate;
        } else {
            newValue = rand - mutationRate;
        }

        if (newValue > 1) newValue = 1;
        else if (newValue < 0) newValue = 0;

        return newValue;
    }

    private float RandomPercentage() {
        float r = UnityEngine.Random.Range(0f, 1f);
        r = Mathf.Round(r * 100f) * 0.01f;
       // Debug.Log(r);
        return r;
    }

    //A normal dice roll
    private bool DiceRoll(int i, int j){
        int rand = UnityEngine.Random.Range(i, j);

        if(rand >= j/2){
            return true;
        }

        return false;
        
    }
    //A dice roll that will return true dependent on m
    private bool DiceRoll(int i, int j, float m){
        int rand = UnityEngine.Random.Range(i, j);

        if(m >= rand){
            return true;
        }

        return false;
        
    }

}
