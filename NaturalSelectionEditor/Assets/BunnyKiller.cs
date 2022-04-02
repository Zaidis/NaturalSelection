using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
public class BunnyKiller : MonoBehaviour
{
    [SerializeField] List<GameObject> decals = new List<GameObject>();
    [SerializeField] GameObject gibblet;
    public static List<GameObject> DECALS = new List<GameObject>();
    public static GameObject GIBBLET;
    private void Awake()
    {
        DECALS = decals;
        GIBBLET = gibblet;
    }
    private void OnTriggerEnter(Collider other)
    {
        KillBunny(other, this.transform, true);
    }

    public static void KillBunny(Collider other, Transform truck, bool truckDecal)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bunny"))
        {
            if (truckDecal)
            {
                GameObject c = Instantiate(DECALS[Random.Range(0, DECALS.Count)], other.transform.position, Quaternion.identity);
                c.transform.LookAt(truck, Vector3.up);
                c.transform.SetParent(truck);
                c.GetComponent<DecalProjector>().decalLayerMask = DecalLayerEnum.DecalLayer1;
            }
            
            //Hit a bunny. DIE
            //0 for truck
            GameObject t = Instantiate(DECALS[Random.Range(0, DECALS.Count)], other.transform.position + Vector3.up, Quaternion.identity);
            t.GetComponent<DecalProjector>().decalLayerMask = DecalLayerEnum.DecalLayer2;
            Instantiate(GIBBLET, other.transform.position + new Vector3(0, 0.5f, 0), other.transform.rotation);
            t.transform.eulerAngles = new Vector3(90, truck.rotation.eulerAngles.y, 0);
            BunnyAI bAI = other.GetComponent<BunnyAI>();
            if (bAI != null)
            {
                bAI.Die();
            }

        }
    }
}
