using System;
using UnityEngine;
using Zenject;

public class TargetMonsterGarden : MonoBehaviour, IHealtheble
{
    protected HealthMonsterGarden _healthMonsterGarden;
    protected AttackMonsterGarden _attackMonsterGarden;
    protected AnimationMonsterGarden _animationMonsterGarden;

    private bool Alife => _healthMonsterGarden.Alife;

    public event Action<TargetMonsterGarden> OnDestroy;

    [Inject]
    public virtual void Construct(HealthMonsterGarden healthMonsterGarden, AttackMonsterGarden attackMonsterGarden, AnimationMonsterGarden animationMonsterGarden)
    {
        _healthMonsterGarden = healthMonsterGarden;
        _attackMonsterGarden = attackMonsterGarden;
        _animationMonsterGarden = animationMonsterGarden;
    }

    private void OnEnable()
    {
        _attackMonsterGarden.OnAttack += OnAttack;
        _attackMonsterGarden.Initialization();
    }

    private void OnDisable()
    {
        _attackMonsterGarden.OnAttack -= OnAttack;
        _animationMonsterGarden.Dispose();
        _attackMonsterGarden.Dispose();
    }

    public void TakeDamage(float damage)
    {
        if (!Alife) { return; }
        _healthMonsterGarden.CheckHealth(damage);
    }

    protected void OnAttack()
    {
        if (!Alife) { return; }
        _attackMonsterGarden.AttackPlayer();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!Alife) { return; }
    //    _attackMonsterGarden.Damage(other);
    //}


    public virtual void OnDead()
    {
        //_animationMonsterGarden.Dispose();
        OnDestroy?.Invoke(this);
        Destroy(gameObject);
    }
}