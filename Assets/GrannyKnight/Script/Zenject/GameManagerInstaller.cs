using UnityEngine;
using Zenject;

public class GameManagerInstaller : MonoInstaller
{
    [SerializeField] private PlayerCharacter _prefPlayer;
    [SerializeField] private Camera _camera;

    public override void InstallBindings()
    {
        BindFactoryPlayer();
        BindCamera();
    }

    //private void BindLoading()
    //{
    //    Container.Bind<ControlLoading>()
    //    .FromInstance(_controlLoading)
    //    .AsSingle();
    //}

    private void BindCamera()
    {
        Container.Bind<Camera>()
       .FromInstance(_camera)
       .AsSingle();
    }

    private void BindFactoryPlayer()
    {
        Container.Bind<FactoryPlayer>()
        .AsSingle()
        .WithArguments(_prefPlayer)
        .NonLazy();
    }
}
