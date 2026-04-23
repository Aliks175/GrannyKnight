using System;
using UnityEngine;

public class DestroyBossGarden : IDisposable
{
    private FactoryMonsterGarden _factoryMonsterGarden;

    public event Action OnEndStage;

    public DestroyBossGarden(FactoryMonsterGarden factoryMonsterGarden)
    {
        _factoryMonsterGarden = factoryMonsterGarden;
    }

    public void Dispose()
    {
        _factoryMonsterGarden.Dispose();
    }

    public void StartStage(Vector3 positionBoss)
    {
        Debug.Log("StartStageTwo");
        CreateBoss(positionBoss);
    }

    private void OnDead(TargetMonsterGarden targetMonsterGarden)
    {
        targetMonsterGarden.OnDestroy -= OnDead;
        OnEndStage?.Invoke();
    }

    private void CreateBoss(Vector3 positionBoss)
    {
        TargetMonsterGarden targetMonsterGarden = _factoryMonsterGarden.GetBoss(positionBoss);
        targetMonsterGarden.OnDestroy += OnDead;
    }
}