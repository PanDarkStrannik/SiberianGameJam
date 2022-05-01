using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class FirstTaskLogic : MonoBehaviour
{
    [ShowInInspector] private List<QuoteOnDoor> _quotes;

    private readonly HashSet<QuoteOnDoor> _alreadyInit = new();

    private FirstDoorTaskData _taskData;

    private void Start()
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
    }


    private void RandomInitialize(string quote)
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
            Debug.Log("Door must be opened!");
        }
    }
}
