using System;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraInputLogic : MonoBehaviour
{
    [SerializeField, Min(0)] private float _movementStep = 0.1f;
    private CinemachineVirtualCamera _camera;

    [SerializeField, Range(0, 1)] private float distanceToShowTask = 0;

    private PlayerInput _playerInput;

    private CinemachineTrackedDolly _trackedDolly;

    private float pathMod = 0;

    private TaskManager _taskManager;

    private void Awake()
    {
        _playerInput = new();
        _camera = GetComponent<CinemachineVirtualCamera>();
        _trackedDolly = _camera.GetCinemachineComponent<CinemachineTrackedDolly>();
    }


    private void Start()
    {
        _taskManager = GameManager.instance.taskManager;
        _taskManager.onGamePassed += OnGameEnd;
        _taskManager.onTaskChanged += CalculatePath;
        CalculatePath();
    }

    private void OnGameEnd()
    {
        _taskManager.onGamePassed -= OnGameEnd;
        _taskManager.onTaskChanged -= CalculatePath;
        Destroy(gameObject);
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
        if(_taskManager.currentTask == null)
            return;
        var showingCurrentTask = _trackedDolly.m_PathPosition >= distanceToShowTask;
        _taskManager.currentTask.SetShow(showingCurrentTask);
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