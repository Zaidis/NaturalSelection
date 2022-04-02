using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerSpawner : MonoBehaviour
{
    Rigidbody caboose;
    [SerializeField] GameObject trailerPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] bool spawnTrailer;

    private void Awake()
    {
        caboose = GetComponent<Rigidbody>();
    }
    
    public void SpawnTrailer()
    {
        GameObject g = Instantiate(trailerPrefab, spawnPoint.position, Quaternion.identity);
        ConfigurableJoint cj = g.GetComponent<ConfigurableJoint>();
        cj.connectedBody = caboose;
        caboose = g.GetComponent<Rigidbody>();
        spawnPoint = g.transform.Find("Spawn Point");
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTrailer)
        {
            spawnTrailer = false;
            SpawnTrailer();
        }
    }
}
