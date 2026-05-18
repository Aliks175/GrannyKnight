using System;
using UnityEngine;

public class FairyAttack
{
    private FactoryBullet _factoryBullet;
    private Transform _body;
    private Transform _target;
    private float _timeNextAttack;
    private float _waitTimeAttack;
    private bool _isAttack;

    public event Action OnPrepareAttack;

    public FairyAttack(FactoryBullet factoryBullet)
    {
        _factoryBullet = factoryBullet;
    }

    public void Start(float waitTimeAttack, Transform body)
    {
        _waitTimeAttack = waitTimeAttack;
        _isAttack = false;
        _body = body;
    }

    public void TryAttack(Collider collider)
    {
        if (_isAttack) { return; }
        if (Time.time > _timeNextAttack)
        {
            OnPrepareAttack?.Invoke();
            _isAttack = true;
            _target = collider.transform;
        }
    }

    public void Attack()
    {
        if (_target == null) { return; }
        _timeNextAttack = Time.time + _waitTimeAttack;
        _isAttack = false;
        Debug.Log($"_target {_target}");
        Bullet tempBullet = _factoryBullet.GetBullet();
        tempBullet.Rigidbody.position = _body.position;

        tempBullet.Rigidbody.AddForce(_target.position - _body.position, ForceMode.VelocityChange);
        _target = null;
        //Debug.Log("OnShoot");
    }


}