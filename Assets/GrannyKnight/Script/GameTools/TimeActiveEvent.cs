using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class TimeActiveEvent : MonoBehaviour, IEventHistoryble
{
    [SerializeField] private float _waitTime;
    [SerializeField] private int _idHistoryData;
    private Coroutine _coroutine;
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

    public void Active()
    {
        if (_coroutine != null) { return; }
        _coroutine = StartCoroutine(WaitTime());
    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(_waitTime);
        OnActiveHistory?.Invoke(_idHistoryData);
    }
}