using UnityEngine;
using Zenject;

public class SystemInstaller : MonoInstaller
{
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private CutsceneScreen _cutsceneScreen;

    public override void InstallBindings()
    {
        BindSystem();
        BindLoadingScreen();
        BindImporter();
        BindLoading();
    }

    private void BindLoading()
    {
        Container.Bind<ControlLoading>()
        .AsSingle();
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

        Container.Bind<CutsceneScreen>()
     .FromInstance(_cutsceneScreen)
     .AsSingle();
    }

    private void BindImporter()
    {
        Container.BindInterfacesAndSelfTo<ImporterGameManagerLoading>()
        .AsSingle()
        .NonLazy();

    }

}