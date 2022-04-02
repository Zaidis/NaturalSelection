using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
public class BunnyKiller : MonoBehaviour
{
    [SerializeField] List<GameObject> decals = new List<GameObject>();
    [SerializeField] GameObject gibblet;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bunny"))
        {
            GameObject c = Instantiate(decals[Random.Range(0, decals.Count)], other.transform.position, Quaternion.identity);
            c.transform.LookAt(this.transform, Vector3.up);
            c.transform.SetParent(this.transform);
            c.GetComponent<DecalProjector>().decalLayerMask = DecalLayerEnum.DecalLayer1;
            //Hit a bunny. DIE
            //0 for truck
            GameObject t = Instantiate(decals[Random.Range(0, decals.Count)], other.transform.position + Vector3.up, Quaternion.identity);
            t.GetComponent<DecalProjector>().decalLayerMask = DecalLayerEnum.DecalLayer2;
            Instantiate(gibblet, other.transform.position + new Vector3(0,0.5f,0), other.transform.rotation);
            t.transform.eulerAngles = new Vector3(90, this.transform.rotation.eulerAngles.y, 0);
            BunnyAI bAI = other.GetComponent<BunnyAI>();
            if (bAI != null) {
                bAI.Die();
            }
            
        }

    }
}
