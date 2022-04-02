using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{

    public List<GameObject> clouds = new List<GameObject>();
    [Range(1, 10)]
    public float cloudRatio;

    [SerializeField] private float spawnTimer = 20;

    private float xRand, yRand, zRand; //cloud position
    private float xRot = 0, yRot, zRot = 0; //cloud rotation
    private int cloudSelection; //for cloud array
    [SerializeField] private Terrain terrain;
    
    private void Update() {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0) {
            SpawnClouds();
        }
    }

    private void SpawnClouds() {
        cloudSelection = Random.Range(0, 4);
        for (int i = 0; i < cloudRatio; i++) {
            xRand = Random.Range(terrain.transform.position.x - 500, terrain.transform.position.x + terrain.terrainData.size.x + 500);
            zRand = Random.Range(terrain.transform.position.z - 500, terrain.transform.position.z + terrain.terrainData.size.z + 500);
            yRand = Random.Range(150, 350);

           
            yRot = Random.Range(0, 360);
            

            GameObject cloud = Instantiate(clouds[cloudSelection], new Vector3(xRand, yRand, zRand), Quaternion.identity);
            cloud.transform.rotation = Quaternion.Euler(xRot, yRot, zRot);
        }

        spawnTimer = 20 - cloudRatio;
    }

}
