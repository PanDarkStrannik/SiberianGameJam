using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class TaskAnimation : MonoBehaviour
{
    [SerializeField]
    private Transform endPostion;

    public void StartAnimation(Action actionAfterAnim)
    {
        var seq = DOTween.Sequence();
        seq.Append(transform.DORotateQuaternion(endPostion.rotation, 1.3f));
        seq.onComplete = actionAfterAnim.Invoke;
    }

}
