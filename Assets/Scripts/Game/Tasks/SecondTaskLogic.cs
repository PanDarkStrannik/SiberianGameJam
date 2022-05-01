using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Canvas))]
public class SecondTaskLogic : MonoBehaviour
{
    private List<ChorusPartPlate> _chorusPartPlates;
    private Canvas _canvas;

    private bool _isDragging;
    private Vector3 _beginDragPos;
    private RectTransform canvasTransform => (RectTransform)_canvas.transform;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _chorusPartPlates = new(GetComponentsInChildren<ChorusPartPlate>());
        switch (_chorusPartPlates.Count)
        {
            case > 4:
                Debug.LogError($"{nameof(ChorusPartPlate)} greater 4 on {gameObject.name}", gameObject);
                return;
            case < 4:
                Debug.LogError($"{nameof(ChorusPartPlate)} less 4 on {gameObject.name}", gameObject);
                return;
        }

        foreach (var plate in _chorusPartPlates)
        {
            plate.Initialize(this);
        }
    }

    public void OnPlateBeginDrag(ChorusPartPlate plate, PointerEventData pointerData)
    {
        _isDragging = true;
        _beginDragPos = plate.transform.position;
    }


    public void OnPlateDrag(ChorusPartPlate plate, PointerEventData eventData)
    {
        if(!_isDragging)
            return;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
                canvasTransform,
                eventData.position,
                Camera.main,
                out var position
            ))
        {
            plate.transform.position = canvasTransform.ClampPosition(
                position,
                plate.worldRect.size
            );
        }
    }

    public void OnPlateEndDrag(ChorusPartPlate plate, PointerEventData pointerData)
    {
        if(!_isDragging)
            return;
        _isDragging = false;
        MovePlate(plate, _beginDragPos);
    }


    private void MovePlate(ChorusPartPlate plate, Vector3 target)
    {
        if(plate.transform.position == target)
            return;
        plate.transform.position = target;
    }


}
