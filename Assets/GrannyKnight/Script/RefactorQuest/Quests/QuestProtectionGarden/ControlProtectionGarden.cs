using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ControlProtectionGarden : IDisposable, IInitializable
{
    private readonly DestroyEnemyGarden _destroyEnemyGarden;
    private readonly DestroyBossGarden _destroyBossGarden;
    private Vector3 _positionBoss;

    public event Action OnEnd;

    public ControlProtectionGarden(DestroyEnemyGarden destroyEnemyGarden, DestroyBossGarden destroyBossGarden)
    {
        _destroyEnemyGarden = destroyEnemyGarden;
        _destroyBossGarden = destroyBossGarden;
    }

    public void Dispose()
    {
        _destroyEnemyGarden.OnEndStage -= OnEndOneStage;
        _destroyBossGarden.OnEndStage -= StopQuest;
        _destroyBossGarden.Dispose();
    }

    public void Initialize()
    {
        _destroyEnemyGarden.OnEndStage += OnEndOneStage;
        _destroyBossGarden.OnEndStage += StopQuest;
    }

    public void StartQuest(List<Transform> _posMonsterSpawn)
    {
        _destroyEnemyGarden.StartStage(_posMonsterSpawn);
        _positionBoss = _posMonsterSpawn[0].position;
    }

    private void StopQuest()
    {
        OnEnd?.Invoke();
    }

    private void OnEndOneStage()
    {
        _destroyBossGarden.StartStage(_positionBoss);
    }
}