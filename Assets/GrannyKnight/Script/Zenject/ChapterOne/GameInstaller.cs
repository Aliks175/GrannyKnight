using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System;

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
    [Header("Квесты объекты")]
    [SerializeField] private QuestPasswordSelection _orderObject;
    [SerializeField] private DragManager _dragObject;

    public override void InstallBindings()
    {
        BindFactoryDust();
        BindPlayerStrategyHealth();
        BindQuestPasswordSelection();
        BindQuestClearBaseMent();
        BindQuestDrag();
    }

    private void BindFactoryDust()
    {
        Container.BindInterfacesAndSelfTo<FactoryDust>()
           .AsSingle()
           .WithArguments(_prefDust, _sizePool)
           .NonLazy();
    }

    private void BindPlayerStrategyHealth()
    {
        Container.BindInterfacesAndSelfTo<BlackoutScreen>()
          .AsSingle()
          .WithArguments(_blackout, _healthPlayer, _timeImmunity, _timeToRegen, _stageHealth)
          .NonLazy();
    }

    private void BindQuestPasswordSelection()
    {
        Container.Bind<QuestPasswordSelection>()
           .FromInstance(_orderObject)
           .AsSingle()
           .NonLazy();
    }
    private void BindQuestDrag()
    {
        Container.Bind<DragManager>()
           .FromInstance(_dragObject)
           .AsSingle()
           .NonLazy();
    }

    private void BindQuestClearBaseMent()
    {
        Container.Bind<ControlTarget>()
           .AsSingle();
    }
}