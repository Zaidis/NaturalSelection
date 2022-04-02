using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorithm : MonoBehaviour
{
    //Smaller value = bigger mutations
    
    public float mutationPercentage;
    public float mutateOverhaulChance;
    
    public void CreateRandomBunny() {
        //make baby
        BunnyStats child = new BunnyStats();

        

    }


    /// <summary>
    /// Called when a male and female bunny give birth. 
    /// </summary>
    /// <param name="male">Male Parent</param>
    /// <param name="female">Female Parent</param>
    public void GiveBirth(BunnyStats male, BunnyStats female){

        float femaleRate = female.fertality * female.foodConsumption;
        float maleRate = male.fertality * male.foodConsumption;
        float birthRate = femaleRate * maleRate;
        
        //most babies = 12 for the average female bunny
        for(int i = 0; i < 12; i++){
            if(DiceRoll(0, 101, birthRate)){
                //you make the baby
                CreateBunny(male, female);
            }
        }   
    }
    public void CreateBunny(BunnyStats dad, BunnyStats mom){
        //make baby
        BunnyStats child = new BunnyStats();

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
        //food consumption
        if(DiceRoll(0, 101, m)){
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
            return (float)Random.Range(0f,0.99f);
        }
        mutationRate /= mutationPercentage; 
        float rand = Random.Range(dadValue, momValue);
        if(DiceRoll(0, 101)){
            return rand + mutationRate;
        } else {
            return rand - mutationRate;
        }
    }

    private float RandomPercentage() {
        return Random.Range(0f, 1f);
    }

    //A normal dice roll
    private bool DiceRoll(int i, int j){
        int rand = Random.Range(i, j);

        if(rand >= j/2){
            return true;
        }

        return false;
        
    }
    //A dice roll that will return true dependent on m
    private bool DiceRoll(int i, int j, float m){
        int rand = Random.Range(i, j);

        if(m >= rand){
            return true;
        }

        return false;
        
    }

}
