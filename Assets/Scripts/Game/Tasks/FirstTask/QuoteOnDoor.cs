using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuoteOnDoor : MonoBehaviour
{
    [SerializeField] private Button _buttonOnDoor;
    [SerializeField] private TextMeshProUGUI _quoteText;

    private Action<QuoteOnDoor> _callback;

    public string quote { get; private set; }

    public void Initialize(string quote, Action<QuoteOnDoor> callback)
    {
        _buttonOnDoor.onClick.AddListener(OnButtonClicked);
        this.quote = quote;
        _quoteText.text = this.quote;
        _callback = callback;
    }

    private void OnButtonClicked()
    {
        _callback.Invoke(this);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}