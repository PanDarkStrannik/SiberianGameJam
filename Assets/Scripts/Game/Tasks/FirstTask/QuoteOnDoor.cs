using System;
using UnityEngine;
using UnityEngine.UI;


public class QuoteOnDoor : MonoBehaviour
{
    [SerializeField] private Image _table;
    [SerializeField] private Button _buttonOnDoor;

    private Action<QuoteOnDoor> _callback;

    public Sprite quote { get; private set; }

    public void Initialize(Sprite quote, Action<QuoteOnDoor> callback)
    {
        _buttonOnDoor.onClick.AddListener(OnButtonClicked);
        this.quote = quote;
        _table.sprite = this.quote;
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

    public void ChangeInteractive(bool isInteractable)
    {
        _table.gameObject.SetActive(isInteractable);
        _buttonOnDoor.interactable = isInteractable;
    }
}