using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStopUI : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _quitButton;


    private void Awake()
    {
        _closeButton.onClick.AddListener(DestroySelf);
        _quitButton.onClick.AddListener(GameSceneManager.instance.Quit);
        StartCoroutine(ToDestroyCorut());
    }


    private IEnumerator ToDestroyCorut()
    {
        yield return new WaitForSecondsRealtime(5f);
        DestroySelf();
    }


    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
