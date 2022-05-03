using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LivesVisualizer : MonoBehaviour
{
    [SerializeField] private Sprite _scullFull;
    [SerializeField] private Sprite _scullDamagedOne;
    [SerializeField] private Sprite _scullDamagedTwo;

    private Image _livesImage;

    private void Start()
    {
        _livesImage = GetComponent<Image>();
        _livesImage.sprite = _scullFull;
    }

    private void OnEnable()
    {
        GameManager.instance.healthManager.onHealthChanged += HealthManager_onHealthChanged;
    }

    private void HealthManager_onHealthChanged(HealthManager obj)
    {
        _livesImage.sprite = obj.currentHealth switch
        {
            3 => _scullFull,
            2 => _scullDamagedOne,
            1 => _scullDamagedTwo,
            _ => _livesImage.sprite
        };
    }

    private void OnDisable()
    {
        GameManager.instance.healthManager.onHealthChanged -= HealthManager_onHealthChanged;
    }
}
