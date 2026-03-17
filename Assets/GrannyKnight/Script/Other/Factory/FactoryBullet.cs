using Refactor;
using System.Collections.Generic;
using UnityEngine;

public class FactoryBullet
{
    private Bullet _prefBullet;
    private GameObject _bulletPool;
    private List<Bullet> _objectPool;

    private float _timeDisable;
    private int _sizePool;
    private int _index;
    private int _damage;

    public FactoryBullet(Bullet prefBullet, int sizePool,int damage,float timeDisable)
    {
        _sizePool = sizePool;
        _prefBullet = prefBullet;
        _bulletPool = new GameObject();
        _bulletPool.name = "BulletPool";
        _objectPool = new();
        _index = 0;
        _damage = damage;
        _timeDisable = timeDisable;
    }

    public Bullet GetBullet()
    {
        Bullet tempgameObject = null;
        CheckBulletPool();
        tempgameObject = GetNextBullet();
        tempgameObject.gameObject.SetActive(true);
        return tempgameObject;
    }

    private void CheckBulletPool()
    {
        if (_objectPool.Count <= 0)
        {
            CreateBulletPool(_sizePool);
        }
    }

    private void CreateBulletPool(int sizePool)
    {
        for (int i = 0; i < sizePool; i++)
        {
            Bullet tempWeapon = GameObject.Instantiate(_prefBullet, Vector3.zero, Quaternion.identity, _bulletPool.transform);
            tempWeapon.Initialization(_timeDisable, _damage);
            tempWeapon.gameObject.SetActive(false);
            _objectPool.Add(tempWeapon);
        }
    }

    private Bullet GetNextBullet()
    {
        Bullet tempgameObject;

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
}