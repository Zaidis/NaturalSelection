using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncleJon : MonoBehaviour
{
    [SerializeField] float detectionRadius, coolDown;
    float timer = 0;
    [SerializeField] LayerMask hitables, bunny;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= coolDown)
        {
            //Fire or try to
            Collider[] c = Physics.OverlapSphere(this.transform.position, detectionRadius, bunny);
            if (c.Length == 0)
                return;
            Collider target = c[0];
            if(c.Length > 0)
            {
                for (int i = 1; i < c.Length; i++)
                {
                    if(Vector3.Distance(this.transform.position, c[i].transform.position) > Vector3.Distance(this.transform.position, target.transform.position))
                    {
                        RaycastHit raycastHit;
                        Physics.Raycast(this.transform.position, c[i].transform.position - this.transform.position, out raycastHit, detectionRadius, hitables, QueryTriggerInteraction.Collide);
                        if(raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("Bunny"))
                        {
                            target = c[i];
                        }
                        
                    }
                }
                Fire(target);
            }

        }
    }

    void Fire(Collider target)
    {
        print(target);
        timer = 0;
        transform.LookAt(target.transform, Vector3.up);
        BunnyKiller.KillBunny(target, this.transform, false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, detectionRadius);
    }
}