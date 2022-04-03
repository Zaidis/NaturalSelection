using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
public class BunnyKiller : MonoBehaviour
{

    public static CameraController cam;
    [SerializeField] List<GameObject> decals = new List<GameObject>();
    [SerializeField] GameObject gibblet;
    public static List<GameObject> DECALS = new List<GameObject>();
    public static GameObject GIBBLET;
    public static int numOfKills;
    [SerializeField] float decalLifeTime;
    static float DECALLIFETIME;

    [SerializeField] AudioClip[] hitSFXs;
    [SerializeField] AudioSource[] audioSources;
    int audioIndex = 0;
    private void Awake()
    {
        DECALS = decals;
        GIBBLET = gibblet;
        DECALLIFETIME = decalLifeTime;
        cam = GetComponent<CameraController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bunny")){
            audioSources[audioIndex].clip = hitSFXs[Random.Range(0, hitSFXs.Length)];
            audioSources[audioIndex].Play();
            audioIndex = (audioIndex + 1) % audioSources.Length;
        }
        

        KillBunny(other, this.transform, true);
    }

    public static void KillBunny(Collider other, Transform truck, bool truckDecal)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bunny"))
        {

            cam.IncreaseZoom();
            numOfKills++;
            Shop.UpdateMoney(4);
            BunnyManager.instance.kills = numOfKills;
            GameManager.instance.CheckVictory(numOfKills);
            TrailerSpawner ts = FindObjectOfType<TrailerSpawner>();
            ts.AddCorpse();
            if (numOfKills % TrailerSpawner.corpsesPerTrailer == 0)
            {
                ts.spawnTrailer = true;
            }
            if (truckDecal)
            {
                SpawnDecal(truck, other.transform, DecalLayerEnum.DecalLayer1);
            }
            SpawnDecalFlat(truck, other.transform, DecalLayerEnum.DecalLayer2, false);
            BunnyAI bAI = other.GetComponent<BunnyAI>();
            if (bAI != null)
            {
                bAI.Die();
            }

        }
    }

    public static void SpawnDecal(Transform hit, Transform other, DecalLayerEnum decalLayerEnum)
    {
        //print("Hit: " + hit.position + " - Hit name: " + hit.name + "other: " + other.position + "other name: " + other.name);
        GameObject c = Instantiate(DECALS[Random.Range(0, DECALS.Count)], other.position, Quaternion.identity);
        c.transform.LookAt(hit, Vector3.up);
        c.transform.SetParent(hit);
        c.GetComponent<DecalProjector>().decalLayerMask = decalLayerEnum;
        Destroy(c, DECALLIFETIME);
    }
    public static void SpawnDecalFlat(Transform hit, Transform other, DecalLayerEnum decalLayerEnum, bool isGib)
    {
        //print("Hit: " + hit.position + " - Hit name: " + hit.name + "other: " + other.position + "other name: " + other.name);
        GameObject t = Instantiate(DECALS[Random.Range(0, DECALS.Count)], other.transform.position + Vector3.up, Quaternion.identity);
        t.GetComponent<DecalProjector>().decalLayerMask = decalLayerEnum;
        if(!isGib)
            Instantiate(GIBBLET, other.transform.position + new Vector3(0, 0.5f, 0), other.transform.rotation);
        t.transform.eulerAngles = new Vector3(90, hit.rotation.eulerAngles.y, 0);
        Destroy(t, DECALLIFETIME);
    }
}
