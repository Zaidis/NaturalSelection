using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailer : MonoBehaviour
{
    public Transform[] corpses;
    [SerializeField] GameObject sawBlades;
    public void ShowCorpse()
    {
        for (int i = 0; i < corpses.Length; i++)
        {
            if (!corpses[i].gameObject.activeInHierarchy)
            {
                corpses[i].gameObject.SetActive(true);
                break;
            }
        }
    }

    private void Update()
    {
        if (Shop.trailersHaveSawBlades && !sawBlades.activeInHierarchy)
        {
            sawBlades.SetActive(true);
        }
    }
}
