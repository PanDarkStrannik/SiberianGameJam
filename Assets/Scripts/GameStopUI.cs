using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStopUI : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _quitButton;


    private void Awake()
    {
        _closeButton.onClick.AddListener(CloseButtonClicked);
        _quitButton.onClick.AddListener(Quit);
        StartCoroutine(ToDestroyCorut());
    }


    private IEnumerator ToDestroyCorut()
    {
        yield return new WaitForSecondsRealtime(5f);
        DestroySelf();
    }


    private void CloseButtonClicked()
    {
        AudioManager.instance.ButtonClickAudio();
        DestroySelf();
    }


    private void Quit()
    {
        AudioManager.instance.ButtonClickAudio();
        GameSceneManager.instance.Quit();
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
