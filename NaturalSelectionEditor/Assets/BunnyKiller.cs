using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyKiller : MonoBehaviour
{
    [SerializeField] List<GameObject> decals = new List<GameObject>();
    [SerializeField] GameObject gibblet;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bunny"))
        {
            //Hit a bunny. DIE
            GameObject t = Instantiate(decals[Random.Range(0, decals.Count)], other.transform.position + Vector3.up, Quaternion.identity);
            Instantiate(gibblet, other.transform.position + new Vector3(0,0.5f,0), other.transform.rotation);
            t.transform.eulerAngles = new Vector3(90, this.transform.rotation.eulerAngles.y, 0);
            Destroy(other.gameObject);
        }

    }
}
