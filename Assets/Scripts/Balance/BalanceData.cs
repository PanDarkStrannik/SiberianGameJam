using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New Balance object", menuName = "Create Balance")]
public class BalanceData : SerializedScriptableObject
{
    [field: SerializeField,TabGroup("Camera Settings"), HideLabel]
    public CameraSettings cameraSettings { get; }

    [field: SerializeField, TabGroup("Tasks")]
    public FirstDoorTaskData firstTask { get; } = new ();

    [field: SerializeField, TabGroup("Tasks")]
    public SecondDoorTaskData secondTask { get; } = new ();

    [field: SerializeField, TabGroup("Tasks")]
    public ThirdDoorTaskData thirdTask { get; } = new();
}


[Serializable]
public struct CameraSettings
{
    [Min(0f)]
    public float movementSpeed;
    [Min(0f)]
    public float rotateSpeed;
}

