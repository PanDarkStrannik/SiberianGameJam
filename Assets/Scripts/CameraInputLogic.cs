using System;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraInputLogic : MonoBehaviour
{
    [SerializeField, Min(0)] private float _movementStep = 0.1f;
    private CinemachineVirtualCamera _camera;

    private PlayerInput _playerInput;

    private CinemachineTrackedDolly _trackedDolly;

    private float pathMod = 0;


    private void Awake()
    {
        _playerInput = new();
        _camera = GetComponent<CinemachineVirtualCamera>();
        _trackedDolly = _camera.GetCinemachineComponent<CinemachineTrackedDolly>();
    }


    private void Start()
    {
        GameManager.instance.taskManager.onTaskChanged += CalculatePath;
        CalculatePath();
    }



    private void CalculatePath()
    {
        _trackedDolly.m_PathPosition = 0;
        var path = (CinemachinePath)_trackedDolly.m_Path;
        path.m_Waypoints[0].position = transform.InverseTransformPoint(Camera.main.transform.position);
        path.m_Waypoints[1].position = transform.InverseTransformPoint(GameManager.instance.taskManager.currentTask.cameraOnTaskPosition);
    }


    private void Update()
    {
        pathMod = _playerInput.WS.WS.ReadValue<float>();
        _trackedDolly.m_PathPosition += _movementStep * pathMod * Time.deltaTime;
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