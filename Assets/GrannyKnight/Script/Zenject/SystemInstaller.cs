using UnityEngine;
using Zenject;

public class SystemInstaller : MonoInstaller
{
    [SerializeField] private LoadingScreen _loadingScreen;

    public override void InstallBindings()
    {
        BindSystem();
        BindLoadingScreen();
        BindImporter();
    }

    private void BindSystem()
    {
        Container.Bind<SystemBuss>()
        .AsSingle()
        .NonLazy();

        Container.Bind<Loading>()
        .AsSingle();

        Container.BindInterfacesAndSelfTo<GameManager>()
       .AsSingle();
    }

    private void BindLoadingScreen()
    {
        Container.Bind<LoadingScreen>()
       .FromInstance(_loadingScreen)
       .AsSingle();
    }

    private void BindImporter()
    {
        Container.Bind<ImporterGameManagerLoading>()
        .AsSingle()
        .NonLazy();

    }

}