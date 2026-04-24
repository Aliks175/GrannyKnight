using System;
using Zenject;

public class ImporterMonsterDestroy : IDisposable, IInitializable
{
    private TargetMonsterGarden _targetMonsterGarden;
    private AnimationMonsterGarden _animationMonsterGarden;

    public ImporterMonsterDestroy(TargetMonsterGarden targetMonsterGarden, AnimationMonsterGarden animationMonsterGarden)
    {
        _animationMonsterGarden = animationMonsterGarden;
        _targetMonsterGarden = targetMonsterGarden;
    }

    public void Dispose()
    {
        _animationMonsterGarden.OnEndAnimationDead -= OnEndAnimationDead;
        _targetMonsterGarden.OnDestroy -= OnDestroy;
    }

    public void Initialize()
    {
        _targetMonsterGarden.OnDestroy += OnDestroy;
        _animationMonsterGarden.OnEndAnimationDead += OnEndAnimationDead;
    }

    private void OnDestroy(TargetMonsterGarden obj)
    {
        _animationMonsterGarden.Dispose();
    }

    private void OnEndAnimationDead()
    {
        _targetMonsterGarden.OnDead();
    }
}