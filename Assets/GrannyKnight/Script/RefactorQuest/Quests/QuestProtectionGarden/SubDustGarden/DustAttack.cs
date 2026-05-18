using DG.Tweening;
using System;
using UnityEngine;

public class DustAttack
{
    private Transform _body;
    private DangerZone _dangerZone;
    private float _timeWaitTouch;
    private float _damage;
    private float _timeNextTouch;
    private int _countTouch;
    private int _maxTouch;
    private bool _isAttack;

    public event Action OnEndAttack;
    public event Action OnAttack;

    public void Start(DangerZone dangerZone, Transform body, int maxTouch, float timeWaitTouch, float damage)
    {
        _dangerZone = dangerZone;
        _body = body;
        _maxTouch = maxTouch;
        _timeWaitTouch = timeWaitTouch;
        _dangerZone.OnEnter += Damage;
        _isAttack = false;
        _damage = damage;
    }

    public void Dispose()
    {
        _dangerZone.OnEnter -= Damage;
    }

    private void Damage(Collider collider)
    {
        Debug.Log($"FindPlayer / _isAttack = {_isAttack}");
        if (_isAttack) { return; }
        _isAttack = true;
        OnPreAttack();
    }

    private void OnPreAttack()
    {
        OnAttack?.Invoke();
    }

    public void Attack()
    {
        if (_dangerZone.CheckPlayer(_body.position, 2, out PlayerCharacter playerCharacter))
        {
            playerCharacter.TakeDamage(_damage);
        }
        _body.DOScale(0.1f, 0.5f)
                .OnComplete(() => OnEndAttack?.Invoke())
                .Play();
    }
}