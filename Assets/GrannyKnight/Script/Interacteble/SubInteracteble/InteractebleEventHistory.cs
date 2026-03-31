using System;
using UnityEngine;
using Zenject;

public class InteractebleEventHistory : Interacteble , IEventHistoryble
{
    [SerializeField] private int _idHistoryData;
    private SystemBuss _systemBuss;
    public event Action<int> OnActiveHistory;

    [Inject]
    public void Construct(SystemBuss systemBuss)
    {
        _systemBuss = systemBuss;
    }

    public void Start()
    {
        _systemBuss.SetEventHistory(this);
    }

    public override void BaseInteract()
    {
        base.BaseInteract();
        ActiveHistory();
    }

    private void ActiveHistory()
    {
        OnActiveHistory?.Invoke(_idHistoryData);
    }
}