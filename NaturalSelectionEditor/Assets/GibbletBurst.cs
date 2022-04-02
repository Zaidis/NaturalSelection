using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GibbletBurst : MonoBehaviour
{
    [SerializeField] GameObject[] heads;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(heads[Random.Range(0, heads.Length)]);
    }

}
