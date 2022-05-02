using UnityEngine;
using UnityEngine.UI;

public class LivesOverUI : MonoBehaviour
{
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _restartButton;

    private void Awake()
    {
        _quitButton.onClick.AddListener(Quit);
        _restartButton.onClick.AddListener(LoadGameScene);
    }


    public void LoadGameScene()
    {
        AudioManager.instance.ButtonClickAudio();
        GameSceneManager.instance.LoadGameScene();
    }

    public void Quit()
    {
        AudioManager.instance.ButtonClickAudio();
        GameSceneManager.instance.Quit();
    }
}
