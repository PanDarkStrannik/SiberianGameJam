using Sirenix.OdinInspector;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField, AssetsOnly] private GameObject pauseUI;
    [SerializeField, AssetsOnly] private GameObject loseUI;

    private GameObject _creatingPause;

    private PlayerInput _input;

    private void Awake()
    {
        _input = new();
    }

    private void EscClicked(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_creatingPause != null)
        {
            Destroy(_creatingPause);
            return;
        }
        _creatingPause = Instantiate(pauseUI);
    }


    public void ShowLoseUI()
    {
        Instantiate(loseUI);
    }


    private void OnEnable()
    {
        _input.Enable();
        _input.Escape.ESC.performed += EscClicked;
    }


    private void OnDisable()
    {
        _input.Disable();
        _input.Escape.ESC.performed -= EscClicked;
    }
}
