using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotLogic : MonoBehaviour
{
    public float yOffset = 0.2f;
    public GameObject carrot;
    public Terrain terrain;

    [SerializeField] private float timer;
    [SerializeField] private float maxTimer = 20;
    private float terrainWidth;
    private float terrainLength;
    private float xTerrain;
    private float zTerrain;

    float boarderBleed = 20f;

    public void Start() {
        timer = maxTimer;
        //Get terrain size
        terrainWidth = terrain.terrainData.size.x;
        terrainLength = terrain.terrainData.size.z;

        //Get terrain position
        xTerrain = terrain.transform.position.x;
        zTerrain = terrain.transform.position.z;

        for (int i = 0; i < 100; i++) SpawnCarrot();

    }

    private void Update() {
        timer -= Time.deltaTime;
        if(timer <= 0) {
            for (int i = 0; i < 100; i++) SpawnCarrot();
            timer = maxTimer;
        }
    }
    private void SpawnCarrot() {
        //Generate random x,z,y position on the terrain
        float x = (Random.Range(xTerrain + boarderBleed, xTerrain + terrainWidth - boarderBleed) + Random.Range(xTerrain + boarderBleed, xTerrain + terrainWidth - boarderBleed))/2f;
        float z = (Random.Range(zTerrain + boarderBleed, zTerrain + terrainLength - boarderBleed) + Random.Range(zTerrain + boarderBleed, zTerrain + terrainLength - boarderBleed))/2f;
        float y = Terrain.activeTerrain.SampleHeight(new Vector3(x, 0, z));

        //Apply Offset if needed
        y += yOffset;

        //Generate the Prefab on the generated position
        GameObject objInstance = Instantiate(carrot, new Vector3(x, y, z), Quaternion.identity);
    }

}
