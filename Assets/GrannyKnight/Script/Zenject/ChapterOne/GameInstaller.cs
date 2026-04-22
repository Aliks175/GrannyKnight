using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Header("QuestDustDestruction")]
    [SerializeField] private TargetDust _prefDust;
    [SerializeField] private int _sizePool;
    [Header("UIDustDestruction")]
    [SerializeField] private Image _blackout;
    [SerializeField] private int[] _stageHealth;
    [SerializeField] private float _timeImmunity;
    [SerializeField] private float _timeToRegen;
    [SerializeField] private int _healthPlayer;

    public override void InstallBindings()
    {
        BindFactoryDust();
        BindPlayerStrategyHealth();
    }

    private void BindPlayerStrategyHealth()
    {
        Container.BindInterfacesAndSelfTo<BlackoutScreen>()
          .AsSingle()
          .WithArguments(_blackout, _healthPlayer, _timeImmunity, _timeToRegen, _stageHealth)
          .NonLazy();
    }

    private void BindFactoryDust()
    {
        Container.BindInterfacesAndSelfTo<FactoryDust>()
           .AsSingle()
           .WithArguments(_prefDust, _sizePool)
           .NonLazy();
    }
}