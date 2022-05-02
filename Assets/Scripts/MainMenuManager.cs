using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button _buttonStart;

    [SerializeField] private Button _buttonExit;
    void Start()
    {
        _buttonStart.onClick.AddListener(GameSceneManager.instance.LoadGameScene);
        _buttonExit.onClick.AddListener(GameSceneManager.instance.Quit);
    }
}
