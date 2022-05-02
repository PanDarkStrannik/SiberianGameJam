using UnityEngine;
using UnityEngine.UI;

public class LivesOverUI : MonoBehaviour
{
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _restartButton;

    private void Awake()
    {
        _quitButton.onClick.AddListener(GameSceneManager.instance.Quit);
        _restartButton.onClick.AddListener(GameSceneManager.instance.LoadGameScene);
    }

}
