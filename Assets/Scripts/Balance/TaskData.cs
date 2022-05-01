using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[HideReferenceObjectPicker]
public class FirstDoorTaskData
{
    [field:SerializeField, TextArea] public string trueQuote { get; }
    [field:SerializeField, TextArea] public string falseQuote1 { get; }
    [field:SerializeField, TextArea] public string falseQuote2 { get; }
}
[HideReferenceObjectPicker]
public class SecondDoorTaskData
{
    [SerializeField, TextArea, ValidateInput(nameof(ValidateList), "Error! Chorus parts greater or less 4")] 
    private List<string> _chorusParts = new(4);
    public IReadOnlyList<string> chorusParts => _chorusParts;

    private bool ValidateList()
    {
        return _chorusParts.Count == 4;
    }
} 

[HideReferenceObjectPicker]
public class ThirdDoorTaskData
{
    [field: SerializeField, Min(1)] public float maxSprayValue { get; } = 1;
    [field: SerializeField, Min(0)] public float sparaySpending { get; } = 0.1f;
}
