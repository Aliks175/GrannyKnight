using UnityEngine;
using Zenject;

public class GameManagerInstaller : MonoInstaller
{
    [SerializeField] private PlayerCharacter _prefPlayer;
   

    public override void InstallBindings()
    {
        BindFactoryPlayer();
        //BindLoading();
    }

    //private void BindLoading()
    //{
    //    Container.Bind<ControlLoading>()
    //    .FromInstance(_controlLoading)
    //    .AsSingle();
    //}

    private void BindFactoryPlayer()
    {
        Container.Bind<FactoryPlayer>()
        .AsSingle()
        .WithArguments(_prefPlayer)
        .NonLazy();
    }
}
