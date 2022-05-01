using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : SerializedMonoBehaviour
{
    [field:SerializeField, InlineEditor] public BalanceData balance { get; }

    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError($"Exist another {nameof(GameManager)} on {instance.name}", instance);
            return;
        }

        instance = this;
    }

}
