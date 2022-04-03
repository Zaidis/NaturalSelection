using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    float additionalZoom = 0f;
    [SerializeField] float zoomMultiplier = .1f;

    [SerializeField] CinemachineVirtualCamera vCam;

    public void IncreaseZoom() {
        additionalZoom += 1f;
        var orbitalTransposer = vCam.GetCinemachineComponent<CinemachineTransposer>();
        orbitalTransposer.m_FollowOffset = new Vector3(0, 5 + additionalZoom* zoomMultiplier, -15 + -2*additionalZoom* zoomMultiplier);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
