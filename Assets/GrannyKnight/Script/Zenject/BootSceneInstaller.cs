using UnityEngine;
using Zenject;

public class BootSceneInstaller : MonoInstaller
{
    //[SerializeField] private GameManager _testGameManager;

    public override void InstallBindings()
    {
        //BindGameManager();
    }

    private void BindGameManager()
    {
        //Container.Bind<GameManager>()
        //   .FromInstance(_testGameManager)
        //   .AsSingle();
    }
}