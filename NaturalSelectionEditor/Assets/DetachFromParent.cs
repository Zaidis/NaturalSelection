using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachFromParent : MonoBehaviour
{
    private void Awake()
    {
        transform.SetParent(null);
    }
}
