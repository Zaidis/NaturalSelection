using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyKiller : MonoBehaviour
{
    [SerializeField] List<GameObject> decals = new List<GameObject>();
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bunny"))
        {
            //Hit a bunny. DIE
            Instantiate(decals[Random.Range(0, decals.Count)], collision.transform.position + Vector3.up, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}
