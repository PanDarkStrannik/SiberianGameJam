using UnityEngine;
using UnityEngine.EventSystems;

public class Drawer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private ThirdTaskLogic _task;

    public void Initialize(ThirdTaskLogic task)
    {
        _task = task;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _task.OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _task.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _task.OnEndDrag(eventData);
    }
}
