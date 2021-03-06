using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class SecondTaskLogic : TaskLogic
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private List<RectTransform> _emptyChoruses;
    [SerializeField] private List<ChorusPartPlate> _chorusPartPlates;

    private bool _isDragging;
    private Vector3 _beginDragPos;
    private RectTransform canvasTransform => (RectTransform)_canvas.transform;

    private SecondDoorTaskData _taskData;

    private Linker<Transform, ChorusPartPlate> _linker=new();

    protected override void StartTaskInternal()
    {
        _taskData = GameManager.instance.balance.secondTask;
        switch (_chorusPartPlates.Count)
        {
            case > 4:
                Debug.LogError($"{nameof(ChorusPartPlate)} greater 4 on {gameObject.name}", gameObject);
                return;
            case < 4:
                Debug.LogError($"{nameof(ChorusPartPlate)} less 4 on {gameObject.name}", gameObject);
                return;
        }

        switch (_emptyChoruses.Count)
        {
            case > 4:
                Debug.LogError($"Empty choruses part greater 4 on {gameObject.name}", gameObject);
                return;
            case < 4:
                Debug.LogError($"Empty choruses part less 4 on {gameObject.name}", gameObject);
                return;
        }

        InitializePlates();
        PlayTem(_taskData);
    }

    protected override void ShowTaskInternal()
    {
        _emptyChoruses.ForEach(empty=>empty.gameObject.SetActive(true));
        _chorusPartPlates.ForEach(chorus=>chorus.gameObject.SetActive(true));
    }

    protected override void HideTaskInternal()
    {
        _emptyChoruses.ForEach(empty => empty.gameObject.SetActive(false));
        _chorusPartPlates.ForEach(chorus => chorus.gameObject.SetActive(false));
    }

    private void InitializePlates()
    {
        var tempParts = new List<Sprite>(_taskData.chorusParts);
        foreach (var plate in _chorusPartPlates)
        {
            var rand = Random.Range(0, tempParts.Count);
            var part = tempParts[rand];
            plate.Initialize(this, part);
            tempParts.Remove(part);
        }

    }


    public void OnPlateBeginDrag(ChorusPartPlate plate, PointerEventData pointerData)
    {
        if(_linker.Contains(plate))
            return;
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

    public void OnPlateEndDrag(ChorusPartPlate plate)
    {
        if(!_isDragging)
            return;
        _isDragging = false;
        var firstOverlaps = _emptyChoruses.FirstOrDefault(empty => empty.GetWorldRect().Contains(plate.transform.position));
        if (firstOverlaps != null)
        {
            var siblingIndex = firstOverlaps.GetSiblingIndex();
            var indexInBalance = _taskData.chorusParts.ToList().IndexOf(plate.chorusPart);
            if (siblingIndex == indexInBalance)
            {
                _linker.Add(firstOverlaps, plate);
                MovePlate(plate, firstOverlaps.transform.position);
                if (_linker.Count == 4)
                {
                    FinishTask();
                }
                return;
            }
            GameManager.instance.healthManager.ApplyDamage();
        }
        MovePlate(plate, _beginDragPos);
    }


    private void MovePlate(ChorusPartPlate plate, Vector3 target)
    {
        if(plate.transform.position == target)
            return;
        plate.transform.position = target;
    }

    public override int taskSortNum => 2;
}
