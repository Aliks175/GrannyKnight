using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FactoryDust
{
    private List<TargetDust> _objectPool;
    private TargetDust _prefDust;
    private DiContainer _container;
    private GameObject _parentPool;
    private int _sizePool;
    private int _index;
    private const string _nameParentDustPool = "DustPool";

    public FactoryDust(DiContainer diContainer,TargetDust prefDust, int sizePool)
    {
        _container = diContainer;
        _sizePool = sizePool;
        _prefDust = prefDust;
        _objectPool = new();
        _index = 0;
    }

    public TargetDust GetDust()
    {
        TargetDust tempgameObject = null;
        CheckDustPool();
        tempgameObject = GetNextDust();
        tempgameObject.gameObject.SetActive(true);
        return tempgameObject;
    }

    private void CheckDustPool()
    {
        if (_objectPool.Count <= 0)
        {
            CheckCreateParent();
            CreateDustPool(_sizePool);
        }
    }

    private void CreateDustPool(int sizePool)
    {
        for (int i = 0; i < sizePool; i++)
        {
            TargetDust tempWeapon = _container.InstantiatePrefabForComponent<TargetDust>(_prefDust, Vector3.zero, Quaternion.identity, _parentPool.transform);

            tempWeapon.gameObject.SetActive(false);
            _objectPool.Add(tempWeapon);
        }
    }

    private TargetDust GetNextDust()
    {
        TargetDust tempgameObject;

        if (_index < _objectPool.Count)
        {
            tempgameObject = _objectPool[_index];
        }
        else
        {
            _index = 0;
            tempgameObject = _objectPool[_index];
        }
        _index++;
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