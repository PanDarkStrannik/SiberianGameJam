using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;


public class FirstTaskLogic : TaskLogic
{
    [SerializeField] private GameObject _helpfullWin;
    [ShowInInspector, ReadOnly] private List<QuoteOnDoor> _quotes;

    private readonly HashSet<QuoteOnDoor> _alreadyInit = new();

    private FirstDoorTaskData _taskData;

    protected override void StartTaskInternal()
    {
        _quotes = new List<QuoteOnDoor>(gameObject.GetComponentsInChildren<QuoteOnDoor>());
        switch (_quotes.Count)
        {
            case < 3:
                Debug.LogError("Quotes less 3");
                return;
            case > 3:
                Debug.LogError("Quotes greater 3");
                return;
        }

        _taskData = GameManager.instance.balance.firstTask;
        RandomInitialize(_taskData.trueQuote);
        RandomInitialize(_taskData.falseQuote1);
        RandomInitialize(_taskData.falseQuote2);
        PlayTem(_taskData);
    }

    protected override void ShowTaskInternal()
    {
        _quotes.ForEach(quote=>quote.ChangeInteractive(true));
    }

    protected override void HideTaskInternal()
    {
        _quotes.ForEach(quote => quote.ChangeInteractive(false));
    }

    private void RandomInitialize(Sprite quote)
    {
        var newRandomList = _quotes.Except(_alreadyInit).ToList();
        var randValue = Random.Range(0, newRandomList.Count);
        var forInit = newRandomList[randValue];
        _alreadyInit.Add(forInit);
        forInit.Initialize(quote,OnQuoteChooseChecker);
    }

    private void OnQuoteChooseChecker(QuoteOnDoor quoteOnDoor)
    {
        if (quoteOnDoor.quote == _taskData.trueQuote)
        {
            FinishTask();
        }
        else
        {
            GameManager.instance.healthManager.ApplyDamage();
            _helpfullWin.gameObject.SetActive(true);
        }
    }

    public override int taskSortNum => 1;
}
