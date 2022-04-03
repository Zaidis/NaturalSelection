using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gibs : MonoBehaviour
{
    public bool spawnedBlood;
    private void Awake()
    {
        transform.SetParent(null);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!spawnedBlood)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                //print("Spawn blood on Ground");
                BunnyKiller.SpawnDecalFlat(this.transform, this.transform, UnityEngine.Rendering.HighDefinition.DecalLayerEnum.DecalLayer2);
                spawnedBlood = true;
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Truck"))
            {
                //print("Spawn blood on Truck");
                BunnyKiller.SpawnDecal(this.transform, collision.transform, UnityEngine.Rendering.HighDefinition.DecalLayerEnum.DecalLayer1);
                spawnedBlood = true;
            }
        }
        
    }
}
