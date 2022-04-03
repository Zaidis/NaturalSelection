using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] Mesh closed;
    [SerializeField] Material closedMat;
    [SerializeField] float lifeTime;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Bunny"))
        {
            //Fire
            GetComponent<MeshRenderer>().material = closedMat;
            GetComponent<MeshFilter>().mesh = closed;
            BunnyKiller.KillBunny(other, this.transform, false);
            GetComponent<BoxCollider>().enabled = false;
            Destroy(this.gameObject, lifeTime);
        }
    }
}
