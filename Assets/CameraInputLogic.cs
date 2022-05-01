using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraInputLogic : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;

    private PlayerInput _playerInput;

    private CinemachineTrackedDolly _trackedDolly;

    private float movementStep => GameManager.instance.balance.cameraSettings.movementSpeed;

    private float pathMod = 0;


    private void Awake()
    {
        _playerInput = new();
        _camera = GetComponent<CinemachineVirtualCamera>();
        _trackedDolly = _camera.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    private void Update()
    {
        pathMod = _playerInput.WS.WS.ReadValue<float>();
        _trackedDolly.m_PathPosition += movementStep * pathMod * Time.deltaTime;
        _trackedDolly.m_PathPosition = Math.Clamp(_trackedDolly.m_PathPosition, 0, 1);
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }


    private void OnDisable()
    {
        _playerInput.Disable();
    }
}