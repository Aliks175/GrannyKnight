using UnityEngine;
using Zenject;

public class UiInstaller : MonoInstaller
{
    [SerializeField] private PlayerUi _prefPlayerUi;

    public override void InstallBindings()
    {
        BindFactoryUi();
    }

    private void BindFactoryUi()
    {
        Container.Bind<FactoryPlayerUi>()
       .AsSingle()
       .WithArguments(_prefPlayerUi)
       .NonLazy();
    }
}