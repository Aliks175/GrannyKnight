using System;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyGarden 
{
    private FactoryMonsterGarden _factoryMonsterGarden;
    private int _countMonster = 0;
    public event Action OnEndStage;

    public DestroyEnemyGarden(FactoryMonsterGarden factoryMonsterGarden)
    {
        _factoryMonsterGarden = factoryMonsterGarden;
    }

    public void StartStage(List<Transform> _posMonsterSpawn)
    {
        //_countMonster = _targetMonsterGardens.Count;
        Debug.Log($"StartQuest - DestroyEnemyGarden = {_countMonster}");
        CreateMonster(_posMonsterSpawn);
    }

    private void OnDead(TargetMonsterGarden targetMonsterGarden)
    {
        targetMonsterGarden.OnDestroy -= OnDead;
        _countMonster--;
        CheckLiveEnemy();
    }

    private void CheckLiveEnemy()
    {
        if (_countMonster <= 0)
        {
            OnEndStage?.Invoke();
        }
        Debug.Log($"_countMonster = {_countMonster}");
    }

    private void CreateMonster(List<Transform> _posMonsterSpawn)
    {
        foreach (var monsterGarden in _posMonsterSpawn)
        {
            TargetMonsterGarden targetMonsterGarden = _factoryMonsterGarden.GetMonster(monsterGarden.position);
            targetMonsterGarden.OnDestroy += OnDead;
            
        }
        _countMonster = _posMonsterSpawn.Count;
    }
}