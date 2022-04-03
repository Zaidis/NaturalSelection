using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailer : MonoBehaviour
{
    public Transform[] corpses;
    
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
}
