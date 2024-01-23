using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject _cameraFront;
    [SerializeField] private GameObject _cameraBack;

    private bool _switchCamera;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SetCamera();
        }
    }
    private void SetCamera()
    {
        _switchCamera = !_switchCamera;

        if (_switchCamera == true)
        {
            _cameraFront.GetComponent<CinemachineVirtualCamera>().Priority = 11;
        }
        
        if (_switchCamera == false)
        {
            _cameraFront.GetComponent<CinemachineVirtualCamera>().Priority = 9;
        }
    }
}
