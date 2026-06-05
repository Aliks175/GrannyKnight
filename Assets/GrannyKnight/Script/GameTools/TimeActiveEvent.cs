using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

public class TimeActiveEvent : MonoBehaviour, IEventHistoryble
{
    [SerializeField] private int _idHistoryData;
    private SystemBuss _systemBuss;
    public event Action<int, IEventHistoryble> OnActiveHistory;

    [Inject]
    public void Construct(SystemBuss systemBuss)
    {
        _systemBuss = systemBuss;
    }

    private void OnDisable()
    {
        _systemBuss.OnEndDialog -= OnEndDialog;
    }

    private void Start()
    {
        _systemBuss.SetEventHistory(this);
    }

    public void Active()
    {
        SubEndDialog().Forget();
    }

    private async UniTaskVoid SubEndDialog()
    {
        await UniTask.NextFrame();
        _systemBuss.OnEndDialog += OnEndDialog;
    }

    private void OnEndDialog()
    {
        _systemBuss.OnEndDialog -= OnEndDialog;
        OnActiveHistory?.Invoke(_idHistoryData, this);
    }
}