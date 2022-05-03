using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform),typeof(Image))]
public class ChorusPartPlate : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private SecondTaskLogic _secondTaskLogic;
    private RectTransform _rectTransform;
    public Rect worldRect => _rectTransform.GetWorldRect();

    public Sprite chorusPart { get; private set; }

    private void Awake()
    {
        _rectTransform = (RectTransform)transform;
    }

    public void Initialize(SecondTaskLogic secondTaskLogic, Sprite chorusPart)
    {
        _secondTaskLogic = secondTaskLogic;
        this.chorusPart = chorusPart;
        var visual = GetComponent<Image>();
        visual.sprite = chorusPart;
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
