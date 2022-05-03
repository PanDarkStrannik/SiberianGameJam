using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button _buttonStart;

    [SerializeField] private Button _buttonExit;
    void Start()
    {
        _buttonStart.onClick.AddListener(StartGame);
        _buttonExit.onClick.AddListener(Quit);
    }

    public void StartGame()
    {
        AudioManager.instance.StartGameClicked();
        GameSceneManager.instance.LoadStartAnimScene();
    }

    public void Quit()
    {
        AudioManager.instance.ButtonClickAudio();
        GameSceneManager.instance.Quit();
    }
}
