using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerSawBlades : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Bunny"))
        {
            BunnyKiller.KillBunny(other, this.transform.root, false);
        }
    }
}
