using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncleJon : MonoBehaviour
{
    [SerializeField] float detectionRadius, coolDown;
    float coolDownRandomizer = 0f;
    float timer = 0;
    [SerializeField] LayerMask hitables, bunny;
    [SerializeField] Transform targetOffset;
    [SerializeField] Transform head, gun;
    [SerializeField] ParticleSystem particles;
    [SerializeField] Animator anim;

    [SerializeField] AudioSource shotGunSound;
    //Lowest fertility ear size and speed added up.
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= coolDown + coolDownRandomizer)
        {
            //Fire or try to
            Collider[] c = Physics.OverlapSphere(targetOffset.position, detectionRadius, bunny);
            if (c.Length == 0)
                return;
            Collider target = c[0];
            if(c.Length > 0)
            {
                BunnyStats bs = c[0].GetComponent<BunnyAI>().stats;
                float targetOverall = bs.fertality + bs.earSize + bs.speed;
                for (int i = 1; i < c.Length; i++)
                {
                    bs = c[i].GetComponent<BunnyAI>().stats;
                    float newTargetStats = bs.fertality + bs.earSize + bs.speed;
                    if (targetOverall < newTargetStats && Vector3.Distance(this.transform.position, c[i].transform.position) > Vector3.Distance(this.transform.position, target.transform.position))
                    {
                        targetOverall = newTargetStats;
                        RaycastHit raycastHit;
                        Physics.Raycast(this.transform.position, c[i].transform.position - this.transform.position, out raycastHit, detectionRadius, hitables, QueryTriggerInteraction.Collide);
                        if(raycastHit.collider != null && raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("Bunny"))
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
        anim.SetTrigger("Fire");
        print(target);
        timer = 0;
        coolDownRandomizer = Random.Range(-coolDown/2f, coolDown/2f);
        particles.Play();
        head.LookAt(target.transform, Vector3.up);
        gun.LookAt(target.transform, Vector3.up);

        shotGunSound.Play();
        BunnyKiller.KillBunny(target, this.transform, false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(targetOffset.position, detectionRadius);
    }
}
