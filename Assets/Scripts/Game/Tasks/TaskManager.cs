using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager
{
    private Stack<TaskLogic> _tasks = new();

    public TaskLogic currentTask { get; private set; }

    public bool gamePassed => _tasks.Count == 0;

    public event Action onGamePassed;
    public event Action onTaskChanged;

    public TaskManager(IEnumerable<TaskLogic> tasks)
    {
        _tasks = new Stack<TaskLogic>(tasks);
        StartNewTask();
    }


    private void StartNewTask()
    {
        currentTask = _tasks.Pop();
        currentTask.StartTask();
        currentTask.onTaskFinished += CurrentTaskFinished;
    }

    private void CurrentTaskFinished(TaskLogic obj)
    {
        if (gamePassed)
        {
            onGamePassed?.Invoke();
            return;
        }
        StartNewTask();
        onTaskChanged?.Invoke();
    }
}

public abstract class TaskLogic : MonoBehaviour
{
    [SerializeField] private Transform _cameraOnTask;
    public Vector3 cameraOnTaskPosition => _cameraOnTask.position;

    public event Action<TaskLogic> onTaskFinished;

    protected TaskState currentState { get; private set; } = TaskState.InActive;

    public void StartTask()
    {
        currentState = TaskState.Active;
    }

    public void FinishTask()
    {
        currentState = TaskState.Finished;
        onTaskFinished?.Invoke(this);
    }

    public enum TaskState
    {
        InActive,
        Active,
        Finished
    }
}
