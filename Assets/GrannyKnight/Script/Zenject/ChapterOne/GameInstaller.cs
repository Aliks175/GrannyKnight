using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private TargetDust _prefDust;
    [SerializeField] private QuestPasswordSelection _orderObject;
    [SerializeField] private int _sizePool;

    public override void InstallBindings()
    {
        BindFactoryDust();
        BindQuestPasswordSelection();
    }

    private void BindFactoryDust()
    {
        Container.BindInterfacesAndSelfTo<FactoryDust>()
           .AsSingle()
           .WithArguments(_prefDust, _sizePool)
           .NonLazy();
    }
    private void BindQuestPasswordSelection()
    {
        Container.Bind<QuestPasswordSelection>()
           .FromInstance(_orderObject)
           .AsSingle()
           .NonLazy();

    }
}