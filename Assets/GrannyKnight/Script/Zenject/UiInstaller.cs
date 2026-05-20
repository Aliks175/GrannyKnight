using UnityEngine;
using Zenject;

public class UiInstaller : MonoInstaller
{
    [SerializeField] private PlayerUi _prefPlayerUi;

    public override void InstallBindings()
    {
        BindFactoryUi();
        BindPromptUi();
        BindImporter();
    }

    private void BindFactoryUi()
    {
        Container.Bind<FactoryPlayerUi>()
       .AsSingle()
       .WithArguments(_prefPlayerUi)
       .NonLazy();
    }

    private void BindPromptUi()
    {
        Container.BindInterfacesAndSelfTo<PromptManager>()
       .AsSingle();
    }

    private void BindImporter()
    {
        Container.BindInterfacesAndSelfTo<PromptManagerQuestImporter>()
              .AsSingle();
    }
}