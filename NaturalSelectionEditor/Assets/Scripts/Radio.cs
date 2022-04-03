using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Radio : MonoBehaviour
{

    [SerializeField] AudioSource audioSource;
    [SerializeField] TextMeshProUGUI staionText;

    [SerializeField] AudioClip[] stations;
    [SerializeField] string[] stationNames;

    int currentStation = 0;

    // Start is called before the first frame update
    void Start()
    {
        staionText.text = stationNames[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying) {
            audioSource.clip = stations[Mathf.Clamp(currentStation, 1, stations.Length)];
            audioSource.Play();
            staionText.text = stationNames[Mathf.Clamp(currentStation, 1, stations.Length)];
        }
    }

    public void ChangeStation(InputAction.CallbackContext context) {
        if (context.performed) {
            currentStation++;
            if (currentStation >= stations.Length)
            {
                currentStation = 1;
            }
            audioSource.clip = stations[Mathf.Clamp(currentStation, 1, stations.Length)];
            audioSource.Play();
            staionText.text = stationNames[Mathf.Clamp(currentStation, 1, stations.Length)];
        }
        
    }
}
