using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[HideReferenceObjectPicker]
public abstract class TaskData
{
    [field: SerializeField, TabGroup("Audio")]
    public AudioClip temeStart { get; private set; }
    [field: SerializeField, TabGroup("Audio")]
    public AudioClip temeLoop { get; private set; }
}

[HideReferenceObjectPicker]
public class FirstDoorTaskData : TaskData
{
    [field:SerializeField, TabGroup("Task"), TextArea] public string trueQuote { get; }
    [field:SerializeField, TabGroup("Task"), TextArea] public string falseQuote1 { get; }
    [field:SerializeField, TabGroup("Task"), TextArea] public string falseQuote2 { get; }
}
[HideReferenceObjectPicker]
public class SecondDoorTaskData : TaskData
{
    [SerializeField, TabGroup("Task"), TextArea, ValidateInput(nameof(ValidateList), "Error! Chorus parts greater or less 4")] 
    private List<string> _chorusParts = new(4);
    public IReadOnlyList<string> chorusParts => _chorusParts;

    private bool ValidateList()
    {
        return _chorusParts.Count == 4;
    }
} 

[HideReferenceObjectPicker]
public class ThirdDoorTaskData : TaskData
{
    [field: SerializeField, TabGroup("Task"), Min(1)] public float maxSprayValue { get; } = 1;
    [field: SerializeField, TabGroup("Task"), Min(0)] public float sparaySpending { get; } = 0.1f;

    [field:SerializeField, TabGroup("Task")] public Material sprayMaterial { get; }
}
