using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public CinemachineFreeLook freeLookCamera;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            freeLookCamera.m_XAxis.m_MaxSpeed = 200;
            freeLookCamera.m_YAxis.m_MaxSpeed = 5;
        }

        if (Input.GetMouseButtonUp(1))
        {
            freeLookCamera.m_XAxis.m_MaxSpeed = 0;
            freeLookCamera.m_YAxis.m_MaxSpeed = 0;
        }
    }
}
