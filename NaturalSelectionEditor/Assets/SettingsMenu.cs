using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject g;
   public void Display()
    {
        if (g.active)
        {
            g.SetActive(false);
        }
        else {
            g.SetActive(true);
        }

    }
}
