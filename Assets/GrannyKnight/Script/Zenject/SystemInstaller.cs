using System;
using UnityEngine;
using Zenject;

public class SystemInstaller : MonoInstaller
{
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private ControlLoading _controlLoading;
    [SerializeField] private Camera _camera;

    public override void InstallBindings()
    {
        BindSystem();
        BindLoadingScreen();
        BindImporter();
        BindCamera();
        BindLoading();
    }

    private void BindLoading()
    {
        Container.Bind<ControlLoading>()
        .FromInstance(_controlLoading)
        .AsSingle();
    }


    private void BindCamera()
    {
        Container.Bind<Camera>()
       .FromInstance(_camera)
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
    }

    private void BindImporter()
    {
        Container.BindInterfacesAndSelfTo<ImporterGameManagerLoading>()
        .AsSingle()
        .NonLazy();

    }

}