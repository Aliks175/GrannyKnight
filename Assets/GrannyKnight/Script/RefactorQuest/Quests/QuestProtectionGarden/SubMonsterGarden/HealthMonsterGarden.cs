using System;
using UnityEngine;

public class HealthMonsterGarden
{
    public bool Alife => isAlife;
    private float _maxHP;
    private float _hP;
    private bool isAlife => _hP > 0;

    public event Action OnDead;
    public event Action OnHit;

    public HealthMonsterGarden()
    {
        _maxHP = UnityEngine.Random.Range(50, 100);
        _hP = _maxHP;
    }

    public void CheckHealth(float damage)
    {
        if (!isAlife) { return; }
        damage = Mathf.Abs(damage);
        _hP -= damage;
        if (_hP > 0)
        {
            OnHit?.Invoke();
            // Анимация
        }
        else
        {
            Dead();
        }
    }

    private void Dead()
    {
        OnDead?.Invoke();
        //    OnDead?.Invoke(this);
        //    gameObject.SetActive(false);
    }
}