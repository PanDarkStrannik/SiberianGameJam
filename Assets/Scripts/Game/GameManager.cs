using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : SerializedMonoBehaviour
{
    [field:SerializeField, InlineEditor] public BalanceData balance { get; }

    public static GameManager instance { get; private set; }

    public HealthManager healthManager { get; private set; }

    public TaskManager taskManager { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError($"Exist another {nameof(GameManager)} on {instance.name}", instance);
            return;
        }

        instance = this;
        Initialize();
    }



    public void Initialize()
    {
        healthManager = new();
        var tasks = FindObjectsOfType<TaskLogic>();
        Debug.Log(tasks.Length);
        taskManager = new TaskManager(tasks);
    }

}
