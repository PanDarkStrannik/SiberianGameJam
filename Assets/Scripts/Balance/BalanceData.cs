using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New Balance object", menuName = "Create Balance")]
public class BalanceData : SerializedScriptableObject
{

    [field: SerializeField, TabGroup("Tasks")]
    public FirstDoorTaskData firstTask { get; } = new ();

    [field: SerializeField, TabGroup("Tasks")]
    public SecondDoorTaskData secondTask { get; } = new ();

    [field: SerializeField, TabGroup("Tasks")]
    public ThirdDoorTaskData thirdTask { get; } = new();

    [field: SerializeField, TabGroup("ShowUp")]
    public float maxDistanceToShow { get; private set; }
    
    [field:SerializeField, TabGroup("ShowUp")]
    public float minDistanceToShow { get; private set; }

    [field: SerializeField, TabGroup("ShowUp")]
    public float alphaChangeStep { get; private set; }
}


