using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerSpawner : Trailer
{
    Rigidbody caboose;
    [SerializeField] GameObject trailerPrefab;
    [SerializeField] Transform spawnPoint;
    public bool spawnTrailer;
    Trailer currentTrailer;
    public static int corpsesPerTrailer;
    private void Awake()
    {
        corpsesPerTrailer = corpses.Length;
        caboose = GetComponent<Rigidbody>();
        currentTrailer = this;
    }
    
    public void SpawnTrailer()
    {
        GameObject g = Instantiate(trailerPrefab, spawnPoint.position, caboose.rotation);
        ConfigurableJoint cj = g.GetComponent<ConfigurableJoint>();
        cj.connectedBody = caboose;
        caboose = g.GetComponent<Rigidbody>();
        spawnPoint = g.transform.Find("Spawn Point");
        currentTrailer = g.GetComponent<Trailer>();
        corpsesPerTrailer = currentTrailer.corpses.Length;

    }
    public void AddCorpse()
    {
        currentTrailer.ShowCorpse();
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
