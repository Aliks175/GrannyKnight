using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FactoryMonsterGarden : IDisposable, IInitializable
{
    private List<TargetMonsterGarden> _objectPool;
    private TargetMonsterGarden _prefMonster;
    private TargetBossGarden _prefBoss;
    private DiContainer _container;
    private GameObject _parentPool;
    private const string _nameParentDustPool = "MonsterGardenPool";

    public FactoryMonsterGarden(DiContainer diContainer, TargetMonsterGarden prefMonster, TargetBossGarden targetBossGarden)
    {
        _container = diContainer;
        _prefMonster = prefMonster;
        _prefBoss = targetBossGarden;
        _objectPool = new();
    }

    public void Dispose()
    {
        //_parentPool.gameObject.SetActive(false);
        GameObject.Destroy(_parentPool);
        _objectPool.Clear();
    }

    public void Initialize()
    {
        CheckCreateParent();
    }

    public TargetMonsterGarden GetMonster(Vector3 position)
    {
        TargetMonsterGarden tempgameObject = _container.InstantiatePrefabForComponent<TargetMonsterGarden>(_prefMonster, position, Quaternion.identity, _parentPool.transform);
        return tempgameObject;
    }

    public TargetBossGarden GetBoss(Vector3 position)
    {
        TargetBossGarden tempgameObject = _container.InstantiatePrefabForComponent<TargetBossGarden>(_prefBoss, position, Quaternion.identity, _parentPool.transform);
        return tempgameObject;
    }

    private void CheckCreateParent()
    {
        _parentPool = GameObject.Find(_nameParentDustPool);
        if (_parentPool == null)
        {
            _parentPool = new GameObject(_nameParentDustPool);
        }
    }
}