using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThirdTaskLogic : TaskLogic
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Drawer _drawer;
    [SerializeField, Min(0.00001f)] private float _minDistanceBetweenPoints;

    private LineRenderer _currentLineRenderer = new();

    private ThirdDoorTaskData _thirdTaskData;

    private float _counter=0;

    protected override void StartTaskInternal()
    {
        _thirdTaskData = GameManager.instance.balance.thirdTask;
        _drawer.Initialize(this);
        PlayTem(_thirdTaskData);
    }

    protected override void ShowTaskInternal()
    {
        _drawer.gameObject.SetActive(true);
    }

    protected override void HideTaskInternal()
    {
        _drawer.gameObject.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var startPoint = CalculateDotFromInputOnCanvas(eventData);

        var newGameObject = new GameObject("Line", typeof(LineRenderer));
        newGameObject.transform.SetParent(_drawer.transform.parent);
        _currentLineRenderer = newGameObject.GetComponent<LineRenderer>();
        _currentLineRenderer.material = _thirdTaskData.sprayMaterial;
        _currentLineRenderer.startWidth = 0.1f;
        _currentLineRenderer.endWidth = 0.1f;
        _currentLineRenderer.positionCount = 0;
        _currentLineRenderer.useWorldSpace = false;
        AddPointToLineRenderer(startPoint);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var calculatedPoint = CalculateDotFromInputOnCanvas(eventData);
        var lastPoint = _currentLineRenderer.GetPosition(_currentLineRenderer.positionCount - 1);
        if (Vector3.Distance(lastPoint,calculatedPoint) >= _minDistanceBetweenPoints)
        {
            AddPointToLineRenderer(calculatedPoint);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var calculatePoint = CalculateDotFromInputOnCanvas(eventData);
        AddPointToLineRenderer(calculatePoint);
    }


    private void AddPointToLineRenderer(Vector3 point)
    {
        _currentLineRenderer.positionCount++;
        _currentLineRenderer.SetPosition(_currentLineRenderer.positionCount - 1, point);
        _counter += _thirdTaskData.sparaySpending;
        if (!(_counter >= _thirdTaskData.maxSprayValue)) return;
        AudioManager.instance.Win();
        FinishTask();
    }

    private Vector3 CalculateDotFromInputOnCanvas(PointerEventData eventData)
    {
        var canvasTransform = (RectTransform)_canvas.transform;
        return RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvasTransform,
            eventData.position,
            Camera.main,
            out var position
        )
            ? canvasTransform.ClampPosition(position,Vector2.zero)
            : canvasTransform.position;
    }

    public override int taskSortNum => 3;
}
