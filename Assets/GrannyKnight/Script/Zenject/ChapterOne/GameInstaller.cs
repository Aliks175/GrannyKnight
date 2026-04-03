using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private TargetDust _prefDust;
    [SerializeField] private int _sizePool;

    public override void InstallBindings()
    {
        BindFactoryDust();
    }

    private void BindFactoryDust()
    {
        Container.BindInterfacesAndSelfTo<FactoryDust>()
           .AsSingle()
           .WithArguments(_prefDust, _sizePool)
           .NonLazy();
    }
}