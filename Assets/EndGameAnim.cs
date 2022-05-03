using DG.Tweening;
using UnityEngine;

public class EndGameAnim : MonoBehaviour
{
    [SerializeField] private RectTransform _imageToIncrease;
    [SerializeField, Min(0.1f)]
    private float _duration;


    private void Start()
    {
        _imageToIncrease.gameObject.SetActive(false);
        GameManager.instance.taskManager.onGamePassed += OnGameEnd;
    }


    private void OnGameEnd()
    {
        _imageToIncrease.gameObject.SetActive(true);
        var seq = DOTween.Sequence();
        seq.Append(_imageToIncrease.DOScale(new Vector3(10f, 10f, 10f), _duration));
        seq.onComplete = OnComplete;
    }

    private void OnComplete()
    {
        GameManager.instance.taskManager.onGamePassed -= OnGameEnd;
        _imageToIncrease.transform.localScale = new Vector3(10f, 10f, 10f);
        GameSceneManager.instance.StartNextScene();
    }
}
