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

    private TaskAnimation _taskAnimation;

    public abstract int taskSortNum { get; }

    protected TaskState currentState { get; private set; } = TaskState.InActive;

    private void Awake()
    {
        _taskAnimation = GetComponentInChildren<TaskAnimation>();
    }

    public void StartTask()
    {
        currentState = TaskState.Active;
        StartTaskInternal();
    }

    protected abstract void StartTaskInternal();

    protected void PlayTem(TaskData tem)
    {
        AudioManager.instance.StartTem(tem.temeStart, tem.temeLoop);
    }

    public void FinishTask()
    {
        currentState = TaskState.Finished;
        onTaskFinished?.Invoke(this);
        _taskAnimation.StartAnimation(KillSelf);
    }

    private void KillSelf()
    {
        Destroy(gameObject);
    }

    public enum TaskState
    {
        InActive,
        Active,
        Finished
    }
}
