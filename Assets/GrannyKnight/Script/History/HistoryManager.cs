using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HistoryManager : MonoBehaviour
{
    [SerializeField] private List<HistoryData> _history;

    public void ActiveHistory(int id)
    {
        HistoryData historyData = FindHistoryData(id);
        if (historyData == null ) { return; }
        historyData.OnInteract?.Invoke();
    }

    private HistoryData FindHistoryData(int id)
    {
        foreach (HistoryData historyData in _history)
        {
            if (historyData.Id == id)
            {
                return historyData;
            }
        }
        return null;
    }
}

[Serializable]
public class HistoryData
{
    [TextArea(2,5)]
    public string Name;
    public int Id;
    public UnityEvent OnInteract;
}