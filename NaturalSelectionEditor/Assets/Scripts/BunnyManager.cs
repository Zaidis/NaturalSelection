using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyManager : MonoBehaviour
{
    public static BunnyManager instance;
    private void Awake()
    {
        if (instance == null){
            instance = this;
        }
        else {
            Destroy(this);
        }
        
    }

    public Transform truck;

    public List<BunnyStats> bunnieStats = new List<BunnyStats>();

    public Terrain terrain;
    private float terrainWidth;
    private float terrainLength;
    private float xTerrain;
    private float zTerrain;

    [SerializeField] int initialPopulation = 100;

    float timer = 0f;

    public int males, females;
    public float fertality, speed, earSize;

    [SerializeField] float graphFrequency = 1f;

    [SerializeField] Graph maleGraph, femaleGraph, fertalityGraph, speedGraph, earSizeGraph;

    void Start(){
        terrainWidth = GameManager.instance.terrain.terrainData.size.x;
        terrainLength = GameManager.instance.terrain.terrainData.size.z;

        //Get terrain position
        xTerrain = GameManager.instance.terrain.transform.position.x;
        zTerrain = GameManager.instance.terrain.transform.position.z;

        for (int i = 0; i < initialPopulation; i++) {
            SpawnBunny();
        }
    }

    void Update(){
        timer += Time.unscaledDeltaTime;
        if (timer >= graphFrequency) {
            timer = 0f;
            UpdateAverages();
            GraphValues();
        }

    }

    void SpawnBunny() {
        float x = Random.Range(xTerrain, xTerrain + terrainWidth);
        float z = Random.Range(zTerrain, zTerrain + terrainLength);
        float y = Terrain.activeTerrain.SampleHeight(new Vector3(x, 0, z));

        //Generate the Prefab on the generated position
        GeneticAlgorithm.instance.CreateRandomBunny(new Vector3(x, y, z));
    }

    void UpdateAverages() {
        int male = 0;
        int fem = 0;

        float f = 0;
        float s = 0;
        float e = 0;

        foreach (BunnyStats b in bunnieStats) {
            if (b.gender == 1){
                male++;
            }
            else {
                fem++;
            }

            f += b.fertality;
            s += b.speed;
            e += b.earSize;
        }

        float count = male + fem;

        fertality = f / count;
        speed = s / count;
        earSize = e / count;

        males = male;
        females = fem;

        if (maleGraph != null) {
            maleGraph.GraphValue(males / 5);
        }
        if (femaleGraph != null){
            femaleGraph.GraphValue(females / 5);
        }

        if (fertalityGraph != null){
            fertalityGraph.GraphValue((int)(fertality * 100));
        }
        if (speedGraph != null){
            speedGraph.GraphValue((int)(speed * 100));
        }
        if (earSizeGraph != null) {
            earSizeGraph.GraphValue((int)(earSize * 100));
        }
        
        

        
        
        

        /*
        maleGraph[graphTimeIndex] = males;
        femaleGraph[graphTimeIndex] = females;

        fertalityGraph[graphTimeIndex] = (int)(fertality * 100);
        speedGraph[graphTimeIndex] = (int)(speed * 100);
        earSizeGraph[graphTimeIndex] = (int)(earSize * 100);
        */

    }

    void GraphValues() {

    }
}
