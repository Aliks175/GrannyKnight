using UnityEngine;
using Zenject;

public class GameManagerInstaller : MonoInstaller
{
    [SerializeField] private TestGameManager _testGameManager;

    public override void InstallBindings()
    {
        BindGameManager();
        BindLoading();
    }

    private void BindGameManager()
    {
        Container.Bind<TestGameManager>()
           .FromInstance(_testGameManager)
           .AsSingle();
    }

    private void BindLoading()
    {
        Container.Bind<TestLoading>()
          .AsSingle();
    }
}