using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class ChorusPartPlate : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private TMP_Text _text;
    private SecondTaskLogic _secondTaskLogic;
    private RectTransform _rectTransform;
    public Rect worldRect => _rectTransform.GetWorldRect();

    public string chorusPart { get; private set; }

    private void Awake()
    {
        _rectTransform = (RectTransform)transform;
    }

    public void Initialize(SecondTaskLogic secondTaskLogic, string chorusPart)
    {
        _secondTaskLogic = secondTaskLogic;
        this.chorusPart = chorusPart;
        _text.text = chorusPart;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _secondTaskLogic.OnPlateBeginDrag(this, eventData);
    }
    public void OnDrag(PointerEventData eventData)
    {
        _secondTaskLogic.OnPlateDrag(this,eventData);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        _secondTaskLogic.OnPlateEndDrag(this);
    }
}
