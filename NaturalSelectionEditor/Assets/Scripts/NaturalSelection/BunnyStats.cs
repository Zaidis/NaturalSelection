using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyStats : MonoBehaviour
{
    public int gender;
    public float fertality;//mom
    public float speed;
    public float intelligence;
    public float earSize; //mom
    public float growthRate;
    public float foodConsumption; //dad
    public float pregnancyDuration;

    public float mutationRate;

    public float jumpHeight; //dad

    public float satiation = 0;

    //behavior weights
    public float scaredWeight;// { get; set; }
    public float hornyWeight;// { get; set; } MOM
    public float hungryWeight;// { get; set; }
    public float boredWeight;// { get; set; }
    public float lazyWeight;// { get; set; }
}
