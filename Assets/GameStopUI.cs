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
    }


    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
