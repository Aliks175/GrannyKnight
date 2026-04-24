using System;
using UnityEngine;
using Zenject;

public class EventHistory : MonoBehaviour , IEventHistoryble
{
    [SerializeField] private int _idHistoryData;
    private SystemBuss _systemBuss;
    public event Action<int> OnActiveHistory;

    [Inject]
    public void Construct(SystemBuss systemBuss)
    {
        _systemBuss = systemBuss;
    }

    private void Start()
    {
        _systemBuss.SetEventHistory(this);
    }

    public void ActiveHistory()
    {
        OnActiveHistory?.Invoke(_idHistoryData);
    }
}