using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject g;
   public void Display()
    {
        g.SetActive(!g.activeInHierarchy);
    }
}
