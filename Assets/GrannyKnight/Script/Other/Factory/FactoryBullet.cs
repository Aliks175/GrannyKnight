using System.Collections.Generic;
using UnityEngine;

public class FactoryBullet
{
    private List<Bullet> _objectPool;
    private Bullet _prefBullet;
    private GameObject _bulletPool;

    private float _timeDisable;
    private int _sizePool;
    private int _index;
    private int _damage;

    private readonly string _nameParentBulletPool;

    public FactoryBullet(Bullet prefBullet, int sizePool, int damage, float timeDisable,string nameParent)
    {
        _sizePool = sizePool;
        _prefBullet = prefBullet;

        _objectPool = new();
        _index = 0;
        _damage = damage;
        _timeDisable = timeDisable;
        _nameParentBulletPool = nameParent;
    }

    public Bullet GetBullet()
    {
        Bullet tempgameObject = null;
        CheckBulletPool();
        tempgameObject = GetNextBullet();
        ControlBullet(tempgameObject);
        tempgameObject.gameObject.SetActive(true);
        return tempgameObject;
    }

    private void CheckBulletPool()
    {
        if (_objectPool.Count <= 0 || _bulletPool == null)
        {
            _objectPool.Clear();
            CheckCreateParent();
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

    private void CheckCreateParent()
    {
        _bulletPool = GameObject.Find(_nameParentBulletPool);
        if (_bulletPool == null)
        {
            _bulletPool = new GameObject(_nameParentBulletPool);
        }
    }

    private void ControlBullet(Bullet tempBullet)
    {
        Rigidbody tempRigidbody = tempBullet.Rigidbody;
        tempRigidbody.angularVelocity = Vector3.zero;
        tempRigidbody.linearVelocity = Vector3.zero;
    }
}