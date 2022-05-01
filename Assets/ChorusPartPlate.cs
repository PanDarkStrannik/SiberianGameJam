using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class ChorusPartPlate : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private SecondTaskLogic _secondTaskLogic;
    private RectTransform _rectTransform;
    public Rect worldRect => _rectTransform.GetWorldRect();
    private void Awake()
    {
        _rectTransform = (RectTransform)transform;
    }

    public void Initialize(SecondTaskLogic secondTaskLogic)
    {
        _secondTaskLogic = secondTaskLogic;
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
        _secondTaskLogic.OnPlateEndDrag(this, eventData);
    }
}
