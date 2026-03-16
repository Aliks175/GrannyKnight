using UnityEngine;
using Zenject;

public class SystemInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindSystem();
    }

    public void BindSystem()
    {
        Container.Bind<SystemBuss>()
      .AsSingle()
      .NonLazy();
    }
}