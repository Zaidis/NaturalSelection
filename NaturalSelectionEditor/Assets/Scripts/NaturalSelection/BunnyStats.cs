using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyStats : MonoBehaviour
{
    
    public float fertality{get; set;} //mom
    public float speed{get; set;}
    public float intelligence{get; set;}
    public float earSize{get; set;} //mom
    public float growth{get; set;} 
    public float foodConsumption{get; set;} //dad
    public float pregnancyDuration{get; set;}
    public int gender;
    public float mutationRate{get; set;}

    public float jumpHeight{get; set;} //dad

    //behavior weights
    public float scaredWeight { get; set; }
    public float hornyWeight { get; set; }
    public float hungryWeight { get; set; }
    public float boredWeight { get; set; }
}
