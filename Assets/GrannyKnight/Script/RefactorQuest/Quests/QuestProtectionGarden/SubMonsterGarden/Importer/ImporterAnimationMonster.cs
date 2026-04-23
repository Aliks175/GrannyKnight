using System;
using Zenject;

public class ImporterAnimationMonster : IDisposable, IInitializable
{
    private HealthMonsterGarden _healthMonsterGarden;
    private AttackMonsterGarden _attackMonsterGarden;
    private AnimationMonsterGarden _animationMonsterGarden;

    public ImporterAnimationMonster(HealthMonsterGarden healthMonsterGarden, AttackMonsterGarden attackMonsterGarden, AnimationMonsterGarden animationMonsterGarden)
    {
        _healthMonsterGarden = healthMonsterGarden;
        _attackMonsterGarden = attackMonsterGarden;
        _animationMonsterGarden = animationMonsterGarden;
    }
    
    public void Dispose()
    {
        _healthMonsterGarden.OnHit -= OnHit;
        _healthMonsterGarden.OnDead -= OnDead;
        _attackMonsterGarden.OnAttack -= OnAttack;
    }

    public void Initialize()
    {
        _healthMonsterGarden.OnHit += OnHit;
        _healthMonsterGarden.OnDead += OnDead;
        _attackMonsterGarden.OnAttack += OnAttack;
    }

    private void OnAttack()
    {
        _animationMonsterGarden.OnAttack();
    }

    private void OnDead()
    {
        _animationMonsterGarden.OnDead();
    }

    private void OnHit()
    {
        _animationMonsterGarden.OnHitDamage();
    }
}
