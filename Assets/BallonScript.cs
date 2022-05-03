using UnityEngine;
using UnityEngine.UI;

public class BallonScript : MonoBehaviour
{
    [SerializeField] private Image _full;

    public static BallonScript instance { get; private set; }

    private void Awake()
    {
        gameObject.SetActive(false);
        instance = this;
    }


    public void ChangeValue(float value)
    {
        _full.fillAmount = value;
    }
}
