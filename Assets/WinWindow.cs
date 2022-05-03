using UnityEngine;
using UnityEngine.UI;

public class WinWindow : MonoBehaviour
{
    [SerializeField] private Button _quit;
    [SerializeField] private Button _restart;


    private void Start()
    {
        _quit.onClick.AddListener(GameSceneManager.instance.Quit);
        _restart.onClick.AddListener(Restart);
    }

    public void Restart()
    {
        GameSceneManager.instance.LoadStartScene();
    }
}
