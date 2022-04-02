using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class GibbletBurst : MonoBehaviour
{
    [SerializeField] GameObject[] heads;
    [SerializeField] float lifeTime, bloodLifeTime;
    VisualEffect blood;
    // Start is called before the first frame update
    void Start()
    {
        blood = GetComponent<VisualEffect>(); 
        Destroy(heads[Random.Range(0, heads.Length)]);
        Destroy(this.gameObject, lifeTime);
        StartCoroutine(Blood());
    }

    IEnumerator Blood()
    {
        yield return new WaitForSecondsRealtime(bloodLifeTime);
        blood.SetFloat("Spawn Rate", 0);
    }
}
